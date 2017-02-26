import { Injectable, Inject } from "@angular/core";
import { Headers, Http, Request, RequestMethod, Response } from "@angular/http";
import { ADPQService, RequestResult, RequestStateEnum, ErrorResponse } from '../shared/adpq.service';
import { AuthService, TokenAuthViewModel } from '../shared/auth.service';
import { CookieService } from 'angular2-cookie/core';
import "rxjs/add/operator/toPromise";
import { Router } from '@angular/router';

export class User {
    userProfileId: number;
    firstName: string;
    lastName: string;
    addressLine1: string;
    city: string;
    state: string;
    zipcode: number;
    phone: string;
    email: string;
    password: string;
    isAdmin: boolean;
    isEmailNotifications: boolean;
    isSms: boolean;
    token: string;
}

@Injectable()
export class UserService {

    private static readonly userProfileUrl = `http://localhost:61552/api/UserProfile`;
    private static readonly tokenAuthUrl = `http://localhost:61552/api/TokenAuth`;

    private adpqService: ADPQService;
    private cookieService: CookieService;
    private authService: AuthService;
    private _loggedInUser: User = new User();
    private _loggedInUserPromise: Promise<User>;

    constructor(private http: Http,
        @Inject(ADPQService) _adpqService: ADPQService, @Inject(CookieService) _cookieService: CookieService,
        @Inject(AuthService) _authService: AuthService, private router: Router) {
        this.adpqService = _adpqService;
        this.cookieService = _cookieService;
        this.authService = _authService;
    }

    public async getLoggedInUser(): Promise<User> {
        let userId = parseInt(this.cookieService.get(AuthService.userIdSessionKey));
        if (!userId) {
            this.logUserOut();
            return;
        }

        if (userId && !this._loggedInUser.email && !this._loggedInUserPromise) {
            this._loggedInUserPromise = this.getUserInfo(userId);
            return this._loggedInUserPromise;
        }
        if (this._loggedInUser && this._loggedInUser.email)
            return Promise.resolve(this._loggedInUser);
        else
            return this._loggedInUserPromise;
    }

    public logUserOut() {
        this._loggedInUser = new User();
        this._loggedInUserPromise = null;
        this.cookieService.removeAll();
        this.router.navigate(['./login']);
    }

    async create(user: User): Promise<User> {
        let response: Response;
        try {
            response = await this.http.post(`http://localhost:61552/api/UserProfile`, user).toPromise();

            let result = response.json() as RequestResult;
            if (result.state == RequestStateEnum.SUCCESS) {
                this._loggedInUser = result.data as User;

                let user = await this.getLoggedInUser();
                this.cookieService.put(AuthService.tokenKey, user.token);
                this.cookieService.put(AuthService.userIdSessionKey, user.userProfileId.toString());
            }
            else {
                this.adpqService.handleNetworkError(<ErrorResponse>{ statusText: result.msg });
                return null;
            }
            return result.data;
        }
        catch (e) {
            this.adpqService.handleNetworkError(e);
        }

    }

    login(userName: string, password: string): Promise<RequestResult> {
        return this.http.post(UserService.tokenAuthUrl, { Email: userName, Password: password }).toPromise()
            .then(response => {
                let result = response.json() as RequestResult;
                if (result.state == RequestStateEnum.SUCCESS) {
                    let json = result.data as TokenAuthViewModel;

                    this.cookieService.put(AuthService.tokenKey, json.token);
                    this.cookieService.put(AuthService.userIdSessionKey, json.userProfileId.toString());
                }
                return result;
            })
            .catch(e => this.adpqService.handleNetworkError(e));
    }

    checkLogin(): boolean {
        let token = this.cookieService.get(AuthService.tokenKey);
        return token != null;
    }

    getUserInfo(id: number): Promise<User> {
        return this.authService.authGet(`${UserService.userProfileUrl}/${id}`).
            then(response => {
                this._loggedInUser = response.data as User;
                return this._loggedInUser;
            })
            .catch((e) => {
                this._loggedInUserPromise = null;
                return null;
            });
    }

    updateUserInfo(user: User): Promise<User> {
        return this.authService.authPut(`${UserService.userProfileUrl}/${user.userProfileId}`, user).
            then(response => {
                this._loggedInUser = response.data as User;
                return this._loggedInUser;
            })
            .catch(e => this.adpqService.handleNetworkError(e));
    }
}