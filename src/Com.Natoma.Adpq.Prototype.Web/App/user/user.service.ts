import { Injectable, Inject } from "@angular/core";
import { Headers, Http, Request, RequestMethod } from "@angular/http";
import { ADPQService, RequestResult, RequestStateEnum } from '../shared/adpq.service';
import { AuthService } from '../shared/auth.service';

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

    constructor(private http: Http, @Inject(ADPQService) _adpqService: ADPQService) {
        this.adpqService = _adpqService;
    }

    async create(user: User): Promise<RequestResult> {
        return this.http.post(`http://localhost:61552/api/UserProfile`, user)
            .toPromise()
            .then(response => {
                let result = response.json() as RequestResult;
                if (result.state == RequestStateEnum.SUCCESS) {
                    let json = result.data as any;

                    sessionStorage.setItem(AuthService.tokenKey, json.token);
                }
                return result;
            })
            .catch(reason => this.adpqService.handleNetworkError(reason));
    }
}