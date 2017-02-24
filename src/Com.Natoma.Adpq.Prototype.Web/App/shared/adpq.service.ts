import { Injectable, Inject } from '@angular/core';
import { Subject } from 'rxjs/Subject';
import { Message } from 'primeng/primeng'; 
import { Response } from "@angular/http";

@Injectable()
export class ADPQService {
    // Observable sources
    private growlSource = new Subject<GrowlObject>();

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
        let detail = error.ovrdDetail;
        let summary = error.ovrdSummary;

        if (!detail)
            detail = `An error occurred: ${error.statusText}`;

        if (!summary)
            summary = "Network Error";

        if (error.status == 422) {
            detail = error._body;
            summary = "An Error Occurred";
        }

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