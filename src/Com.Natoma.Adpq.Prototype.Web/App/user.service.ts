import { Injectable, Inject } from "@angular/core";
import { Headers, Http, Request, RequestMethod } from "@angular/http";
import { ADPQService } from './shared/adpq.service';

import "rxjs/add/operator/toPromise";

export class User {
    email: string;
    password: string;
    firstName: string;
    lastName: string;

    addressLine1: string;
    city: string;
    zipcode: number;
    state: string;

    doReceiveEmailNotifications: boolean;
}

@Injectable()
export class UserService {

    private adpqService: ADPQService;

    constructor(private http: Http, @Inject(ADPQService) _adpqService: ADPQService) {
        this.adpqService = _adpqService;
    }

    async create(user: User): Promise<User> {
        return this.http.post(`http://localhost:61552/api/UserProfile`, user)
            .toPromise()
            .then(response => response.json() as User)
            .catch(reason => this.adpqService.handleNetworkError(reason));
    }
}