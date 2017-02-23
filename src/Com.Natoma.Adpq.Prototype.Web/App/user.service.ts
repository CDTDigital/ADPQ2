import { Injectable, Inject } from "@angular/core";
import { Headers, Http, Request, RequestMethod } from "@angular/http";

import "rxjs/add/operator/toPromise";

export class User {
    email: string;
    password: string;
    firstName: string;
    lastName: string;

    addressLine1: string;
    city: string;
    zipcode: number;

    doReceiveEmailNotifications: boolean;
}

@Injectable()
export class UserService {

    constructor(private http: Http) { }

    async create(user: User): Promise<any> {
        return this.http.post(`http://localhost:61552/api/UserProfile`, user)
            .toPromise()
            .then(response => response.json().result as User);
            //.catch(reason => this.ermaService.handleNetworkError(reason));
    }
}