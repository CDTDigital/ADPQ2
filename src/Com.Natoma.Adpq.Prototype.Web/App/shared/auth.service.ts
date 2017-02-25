import { Injectable } from "@angular/core";
import { Headers, Http } from "@angular/http";
import { ADPQService, GrowlObject, ErrorResponse, RequestStateEnum, RequestResult } from './adpq.service';
import { UserService, User } from '../user/user.service';
import "rxjs/add/operator/toPromise";
import { CookieService } from 'angular2-cookie/core';

export class TokenAuthViewModel {
    userProfileId: number;
    token: string;
}

@Injectable()
export class AuthService {
    public static readonly tokenKey = "adpqtoken";
    public static readonly userIdSessionKey = "adpquserid";

    private static readonly userProfileUrl = `http://localhost:61552/api/UserProfile`;
    private static readonly tokenAuthUrl = `http://localhost:61552/api/TokenAuth`;


    constructor(private http: Http, private adpqService: ADPQService, private cookieService: CookieService) { }

    login(userName: string, password: string): Promise<RequestResult> {
        return this.http.post(AuthService.tokenAuthUrl, { Email: userName, Password: password }).toPromise()
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
        return this.authGet(`${AuthService.userProfileUrl}/${id}`).
            then(response => response.data as User);
    }

    updateUserInfo(user: User): Promise<User> {
        return this.authPut(`${AuthService.userProfileUrl}/${user.userProfileId}`, user).
            then(response => response.data as User)
            .catch(e => this.adpqService.handleNetworkError(e));
    }

    ////////////////////////////////////////
    // AUTH
    ////////////////////////////////////////
    authPut(url: string, body: any): Promise<RequestResult> {
        let headers = this.initAuthHeaders();
        return this.http.put(url, body, { headers: headers }).toPromise()
            .then(response => response.json() as RequestResult)
            .catch(e => this.adpqService.handleNetworkError(e));
    }

    authGet(url: string): Promise<RequestResult> {
        let headers = this.initAuthHeaders();
        return this.http.get(url, { headers: headers }).toPromise()
            .then(response => response.json() as RequestResult)
            .catch(e => this.adpqService.handleNetworkError(e));
    }

    private initAuthHeaders(): Headers {
        let token = this.cookieService.get(AuthService.tokenKey);
        if (token == null) throw "No token";

        var headers = new Headers();
        headers.append("Authorization", "Bearer " + token);

        return headers;
    }
}