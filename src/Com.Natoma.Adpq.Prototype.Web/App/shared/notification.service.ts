import { Injectable, Inject } from "@angular/core";
import { Headers, Http } from "@angular/http";
import { ADPQService, RequestResult, RequestStateEnum, ErrorResponse } from './adpq.service';
import "rxjs/add/operator/toPromise";
import { AuthService, TokenAuthViewModel } from '../shared/auth.service';
import { UserService } from '../user/user.service';

export class Notification {
    notificationId: number;
    notificationType: NotificationGeoType;
    addressLine1: string;
    city: string;
    zipcode: string;
    state: string;
    radiusMiles: number;
    numberOfRecipients: number;
    createdBy: number;

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
    BLAST = 1,
    REGIONAL = 2
}

export class UserNotificationViewModel {
    userNotificationId: number;
    notificationId: number;
    notificationSubject: string;
    notificationMessage: string;
    notificationSmsMessage: string;
    isEmail: boolean;
    isSms: boolean;
    result: string;
    createdOn: string;  
    notificationTypeId: number;
}

export enum DeliveryMethodEnum {
    EMAIL,
    SMS
}

export class NotificationMetricsViewModel {
    dateSent: string;
    sendType: DeliveryMethodEnum;
    sendTypeDisplay: string;
    count: number
    dateSentDisplay: string;
}

@Injectable()
export class NotificationService {
    private authService: AuthService;
    private userService: UserService;

    constructor(private http: Http, private adpqService: ADPQService, @Inject(AuthService) _authService: AuthService,
        @Inject(UserService) _userService: UserService) {
        this.authService = _authService;
        this.userService = _userService;
    }

    async postNotification(notification: Notification): Promise<Notification> {
        notification.createdBy = (await this.userService.getLoggedInUser()).userProfileId; 

        return this.authService.authPost(`${ADPQService.apiUrl}/api/Notification`, notification).
            then(response => {
                if (response.state == RequestStateEnum.SUCCESS)
                    return new Notification();
                else {
                    this.adpqService.growl({ severity: 'error', summary: `Server Error`, detail: response.msg });
                    return null;
                }
            });
    }

    async getNotificationsForUser(userId: number): Promise<Notification[]> {
        return this.authService.authGet(`${ADPQService.apiUrl}/api/Notification/${userId}`).
            then(response => {
                if (response.state == RequestStateEnum.SUCCESS)
                    return response.data as Notification[];
                else {
                    this.adpqService.growl({ severity: 'error', summary: `Server Error`, detail: response.msg });
                    return null;
                }
            });
    }

    async getMetricsData(): Promise<NotificationMetricsViewModel[]> {
        return this.authService.authGet(`${ADPQService.apiUrl}/api/Notification/30DayReport`).
            then(response => {
                if (response.state == RequestStateEnum.SUCCESS)
                    return response.data as NotificationMetricsViewModel[];
                else {
                    this.adpqService.growl({ severity: 'error', summary: `Server Error`, detail: response.msg });
                    return null;
                }
            });
    }
}