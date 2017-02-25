import { Component, OnInit, OnDestroy } from '@angular/core';

import { Subscription } from 'rxjs/Subscription';
import { Message, MenuItem } from 'primeng/primeng';
import { ADPQService, GrowlObject } from './shared/adpq.service';
import { UserService, User } from './user/user.service';

@Component({
    selector: 'adpq-app',
    templateUrl: '../html/home.html'
})
export class AppComponent implements OnDestroy {
    private growls: Message[] = [];
    private stickyGrowls: Message[] = [];

    onGrowlSub: Subscription;
    homeIcon = <MenuItem>{ routerLink: ['home'] };
    user: User;

    constructor(private adpqService: ADPQService, private userService: UserService) {
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

    async isUserLoggedIn() {
        let user = await this.userService.getLoggedInUser();
        if (user.email)
            return true
        else
            return false;
    }

    ngOnDestroy() {
        this.onGrowlSub.unsubscribe();
    }
}