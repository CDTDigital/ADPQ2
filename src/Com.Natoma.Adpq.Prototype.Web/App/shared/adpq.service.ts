import { Injectable, Inject } from '@angular/core';
import { Subject } from 'rxjs/Subject';
import { Message } from 'primeng/primeng'; 

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
}

export class GrowlObject {
    constructor(public message: Message, public isSticky: boolean) { }
}