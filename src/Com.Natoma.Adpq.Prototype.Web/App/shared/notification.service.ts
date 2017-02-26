import { Injectable, Inject } from "@angular/core";
import { Headers, Http } from "@angular/http";
import { ADPQService, RequestResult } from './adpq.service';
import "rxjs/add/operator/toPromise";
import { AuthService, TokenAuthViewModel } from '../shared/auth.service';

export class Notification {
    notificationId: number;
    notificationType: NotificationGeoType;
    addressLine1: string;
    city: string;
    zip: string;
    state: string;
    radiusMiles: number;
    numberOfRecipients: number;

    emailSubject: string;
    emailMessage: string;

    smsMessage: string;

    latitude: number;
    longitude: number;

    constructor() {
        this.radiusMiles = 20;
        this.notificationType = NotificationGeoType.BLAST;
        this.emailMessage = "";
        this.emailSubject = "";
    }
}
export enum NotificationGeoType {
    BLAST,
    REGIONAL
}

@Injectable()
export class NotificationService {
    private static readonly notificationsUrl = `http://localhost:61552/api/Notification`;

    private authService: AuthService;

    constructor(private http: Http, private adpqService: ADPQService, @Inject(AuthService) _authService: AuthService) {
        this.authService = _authService;
    }

    async postNotification(notification: Notification): Promise<Notification> {
        return this.authService.authPost(`${NotificationService.notificationsUrl}`, notification).
            then(response => response.data as Notification)
            .catch(e => this.adpqService.handleNetworkError(e));
    }
}