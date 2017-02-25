import { Component, OnInit, OnDestroy } from '@angular/core';

import { Subscription } from 'rxjs/Subscription';
import { Message, MenuItem } from 'primeng/primeng';
import { ADPQService, GrowlObject } from './shared/adpq.service';

@Component({
    selector: 'adpq-app',
    templateUrl: '../html/home.html'
})
export class AppComponent implements OnInit, OnDestroy {
    private growls: Message[] = [];
    private stickyGrowls: Message[] = [];

    onGrowlSub: Subscription;
    homeIcon = <MenuItem>{ routerLink: ['home'] };

    constructor(private adpqService: ADPQService) {
        this.growls = [];

        // This receives the message that a growl needs to be displayed
        this.onGrowlSub = adpqService.growl$.subscribe((growlObj: GrowlObject) => {
            if (!growlObj || growlObj == null) {
                this.stickyGrowls = [];
                this.growls = [];
                return;
            }

            if (growlObj.isSticky)
                this.stickyGrowls.push(growlObj.message);
            else
                this.growls.push(growlObj.message);
        });
    }

    ngOnInit() {
    }

    ngOnDestroy() {
        this.onGrowlSub.unsubscribe();
    }
}