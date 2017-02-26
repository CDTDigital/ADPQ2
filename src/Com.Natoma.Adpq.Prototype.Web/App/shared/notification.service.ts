import { Injectable } from "@angular/core";
import { Headers, Http } from "@angular/http";
import { ADPQService, RequestResult } from './adpq.service';
import "rxjs/add/operator/toPromise";

export class Notification {
    type: NotificationType;
    geoType: NotificationGeoType;
    refStreet: string;
    refCity: string;
    refZip: string;
    radius: number;

    emailSubject: string;
    emailMessage: string;

    textMessage: string;

    constructor() {
        this.radius = 20;
        this.geoType = NotificationGeoType.BLAST;
        this.emailMessage = "";
        this.emailSubject = "";
    }
}
export enum NotificationGeoType {
    BLAST,
    REGIONAL
}
export enum NotificationType {
    SMS,
    EMAIL
}

@Injectable()
export class NotificationService {

    constructor(private http: Http, private adpqService: ADPQService) { }

    async postNotification(notification: Notification): Promise<RequestResult> {
        return null;

        //return this.authService.authPost(`${UserService.userProfileUrl}/${user.userProfileId}`, user).
        //    then(response => {
        //        this._loggedInUser = response.data as User;
        //        return this._loggedInUser;
        //    })
        //    .catch(e => this.adpqService.handleNetworkError(e));
    }
}