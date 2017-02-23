import { Injectable, Inject } from "@angular/core";
import { Headers, Http, Request, RequestMethod } from "@angular/http";

import "rxjs/add/operator/toPromise";

export class User {
    emailAdress: string;
    password: string;
    firstName: string;
    lastName: string;

    addressLine: string;
    city: string;
    zip: number;

    doReceiveEmailNotifications: boolean;
}

@Injectable()
export class UserService {
}