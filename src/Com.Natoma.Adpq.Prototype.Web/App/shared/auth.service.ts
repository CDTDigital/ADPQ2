import { Injectable } from "@angular/core";
import { Headers, Http } from "@angular/http";
import { ADPQService, GrowlObject, ErrorResponse, RequestStateEnum, RequestResult } from './adpq.service';
import { UserService } from '../user/user.service';
import "rxjs/add/operator/toPromise";

export class TokenAuthViewModel {
    userProfileId: number;
    token: string;
}

@Injectable()
export class AuthService {
    public static readonly tokenKey = "adpqtoken";
    private token: string;

    constructor(private http: Http, private adpqService: ADPQService) { }

    login(userName: string, password: string): Promise<RequestResult> {
        return this.http.post("http://localhost:61552/api/TokenAuth", { Email: userName, Password: password }).toPromise()
            .then(response => {
                let result = response.json() as RequestResult;
                if (result.state == RequestStateEnum.SUCCESS) {
                    let json = result.data as TokenAuthViewModel;

                    sessionStorage.setItem(AuthService.tokenKey, json.token);
                    sessionStorage.setItem(UserService.userIdSessionKey, json.userProfileId.toString());
                }
                return result;
            })
            .catch(e => this.adpqService.handleNetworkError(e));
    }

    logout() { }

    checkLogin(): boolean {
        var token = sessionStorage.getItem(AuthService.tokenKey);
        return token != null;
    }

    getUserInfo(id: number): Promise<RequestResult> {
        return this.authGet(`http://localhost:61552/api/UserProfile/${id}`);
    }

    authPost(url: string, body: any): Promise<RequestResult> {
        let headers = this.initAuthHeaders();
        return this.http.post(url, body, { headers: headers }).toPromise()
            .then(response => response.json() as RequestResult)
            .catch(e => this.adpqService.handleNetworkError(e));
    }

    authGet(url): Promise<RequestResult> {
        let headers = this.initAuthHeaders();
        return this.http.get(url, { headers: headers }).toPromise()
            .then(response => response.json() as RequestResult)
            .catch(e => this.adpqService.handleNetworkError(e));
    }

    private getLocalToken(): string {
        if (!this.token) {
            this.token = sessionStorage.getItem(AuthService.tokenKey);
        }
        return this.token;
    }

    private initAuthHeaders(): Headers {
        let token = this.getLocalToken();
        if (token == null) throw "No token";

        var headers = new Headers();
        headers.append("Authorization", "Bearer " + token);

        return headers;
    }
}