import { Component, OnInit } from '@angular/core';
import { User } from './user.service';
import { AuthService } from './shared/auth.service';
import { UserService } from './user.service';
import { ADPQService, GrowlObject } from './shared/adpq.service';
import { Message, SelectItem } from 'primeng/primeng';
import { StatesFactory } from './shared/states';

@Component({
    selector: 'adpq-login',
    templateUrl: '../html/login.component.html',
    styleUrls: ['../css/login.component.css'],
    moduleId: module.id,
})
export class LoginComponent implements OnInit {

    user: User = new User();
    newUser: User = new User();
    infoMessages: Message[] = [];

    isSignupVisible = false;
    states: SelectItem[] = StatesFactory.getStatesAsSelectItems();
    selectedStateIdx = 4;

    constructor(private authService: AuthService, private userService: UserService, private adpqService: ADPQService) { }

    ngOnInit() {
    }

    doMakeSignupVisible() {
        this.user = new User();
        this.isSignupVisible = true;
    }

    doLogin() {
        this.authService.login(this.user.email, this.user.password)
            .then(result => {
                if (result.State == 1) {
                    //this.router.navigate(["./home"]);
                    console.log("OK!");
                }
                else {
                    this.adpqService.growl(<Message>{ summary: result.Msg, detail: "Server Error", severity: "error" });
                }
            });
    }

    async doSignup(save: boolean) {
        if (!save)
            this.newUser = new User();
        else {
            this.newUser.state = StatesFactory.getStates()[this.selectedStateIdx].shortName;
            let user = await this.userService.create(this.newUser);
            this.infoMessages.push({ severity: 'success', summary: `Thank you ${user.firstName}! You can now log in.`, detail: 'User successfully created' });
            this.newUser = new User();
        }
        this.isSignupVisible = false;
    }
}