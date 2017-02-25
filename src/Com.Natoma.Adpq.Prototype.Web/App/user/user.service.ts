import { Injectable, Inject } from "@angular/core";
import { Headers, Http, Request, RequestMethod } from "@angular/http";
import { ADPQService, RequestResult, RequestStateEnum } from '../shared/adpq.service';
import { AuthService } from '../shared/auth.service';
import { CookieService } from 'angular2-cookie/core';
import "rxjs/add/operator/toPromise";

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

    private adpqService: ADPQService;
    private cookieService: CookieService;
    private authService: AuthService;
    private alreadyQueried = false;

    private _loggedInUser: User = new User();
    public get loggedInUser(): User {
        if (!this._loggedInUser.email && !this.alreadyQueried) {
            this.alreadyQueried = true;
            this.authService.getUserInfo(parseInt(this.cookieService.get(AuthService.userIdSessionKey))).then((user) => {
                this._loggedInUser = user;
            });
        }
        return this._loggedInUser;
    }
    public set loggedInUser(val: User) {
        this._loggedInUser = val;
    }

    constructor(private http: Http,
        @Inject(ADPQService) _adpqService: ADPQService, @Inject(CookieService) _cookieService: CookieService, @Inject(AuthService) _authService: AuthService) {
        this.adpqService = _adpqService;
        this.cookieService = _cookieService;
        this.authService = _authService;
    }

    async create(user: User): Promise<RequestResult> {
        return this.http.post(`http://localhost:61552/api/UserProfile`, user)
            .toPromise()
            .then(response => {
                let result = response.json() as RequestResult;
                if (result.state == RequestStateEnum.SUCCESS) {
                    this.loggedInUser = result.data as User;

                    this.cookieService.put(AuthService.tokenKey, this.loggedInUser.token);
                    this.cookieService.put(AuthService.userIdSessionKey, this.loggedInUser.userProfileId.toString());
                }
                return result;
            })
            .catch(reason => this.adpqService.handleNetworkError(reason));
    }
}