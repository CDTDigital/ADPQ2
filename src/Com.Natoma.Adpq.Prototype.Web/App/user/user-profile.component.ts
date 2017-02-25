import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { StatesFactory } from '../shared/states';
import { ADPQService, GrowlObject } from '../shared/adpq.service';
import { User, UserService } from './user.service';
import { SelectItem } from 'primeng/primeng';
import { UserFormViewModel } from './user-form.component';
import { AuthService } from '../shared/auth.service';

@Component({
    templateUrl: '../../html/user-profile.component.html',
    moduleId: module.id,
})
export class UserProfileComponent implements OnInit {
    
    constructor(private userService: UserService, private adpqService: ADPQService, private authService: AuthService) { }

    ngOnInit() {
    }

    async onSignupFormSubmit(vm: UserFormViewModel) {
        if (vm && vm.user) {
            vm.user.state = StatesFactory.getStates()[vm.selectedStateIdx].shortName;
            await this.authService.updateUserInfo(vm.user);
            this.adpqService.growl({ severity: 'success', summary: `Profile successfully updated` });
            vm.user = new User();
        }
    }
}