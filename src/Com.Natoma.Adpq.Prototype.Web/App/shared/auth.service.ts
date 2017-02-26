import { Injectable } from "@angular/core";
import { Headers, Http } from "@angular/http";
import { ADPQService, RequestResult } from './adpq.service';
import "rxjs/add/operator/toPromise";
import { CookieService } from 'angular2-cookie/core';

export class TokenAuthViewModel {
    userProfileId: number;
    token: string;
    isAdmin: boolean;
}

@Injectable()
export class AuthService {
    public static readonly tokenKey = "adpqtoken";
    public static readonly userIdSessionKey = "adpquserid";

    constructor(private http: Http, private adpqService: ADPQService, private cookieService: CookieService) { }

    authPut(url: string, body: any): Promise<RequestResult> {
        let headers = this.initAuthHeaders();
        return this.http.put(url, body, { headers: headers }).toPromise()
            .then(response => response.json() as RequestResult)
            .catch(e => this.adpqService.handleNetworkError(e));
    }

    authPost(url: string, body: any): Promise<RequestResult> {
        let headers = this.initAuthHeaders();
        return this.http.post(url, body, { headers: headers }).toPromise()
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