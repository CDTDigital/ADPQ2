import { Injectable, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { Subject } from 'rxjs/Subject';
import { Message } from 'primeng/primeng';
import { Response } from "@angular/http";
import { CookieService } from 'angular2-cookie/core';
import { MenuItem } from 'primeng/primeng';

export enum RequestStateEnum {
    FAILED = -1,
    NOT_AUTH = 0,
    SUCCESS = 1
}

export class RequestResult {
    state: RequestStateEnum;
    msg: string;
    data: any;
}

@Injectable()
export class ADPQService {
    private cookieService: CookieService;
    breadcrumbItems: MenuItem[] = [];
    static readonly apiUrl = `http://localhost:61552`;

    // Observable sources
    private growlSource = new Subject<GrowlObject>();

    constructor(private router: Router, @Inject(CookieService) _cookieService: CookieService) {
        this.cookieService = _cookieService;
    }

    // Observable string streams
    growl$ = this.growlSource.asObservable();

    // Service message commands
    growl(message: Message, isSticky: boolean = false) {
        if (!message || message == null)
            this.growlSource.next(null);
        else
            this.growlSource.next(new GrowlObject(message, isSticky));
    }

    handleNetworkError(error: ErrorResponse) {
        if (error.status == 401) {
            this.cookieService.removeAll();
            this.router.navigate(['./login']);
        }

        let detail = error.ovrdDetail;
        let summary = error.ovrdSummary;

        if (!detail)
            detail = `An error occurred: ${error.statusText}`;

        if (!summary)
            summary = "Network Error";

        this.growl({ severity: "error", summary: summary, detail: detail });
        console.error('An error occurred', error);
        return Promise.reject(error.message || error);
    }
}

export class GrowlObject {
    constructor(public message: Message, public isSticky: boolean) { }
}

export class ErrorResponse extends Response {
    _body: string;
    message: string;
    ovrdSummary: string;
    ovrdDetail: string;
}