import { Component, OnInit } from '@angular/core';
import { StatesFactory } from '../shared/states';
import { ADPQService } from '../shared/adpq.service';
import { User, UserService } from './user.service';
import { UserFormViewModel } from './user-form.component';
import { Router } from '@angular/router';

@Component({
    templateUrl: '../../html/user-profile.component.html',
    moduleId: module.id,
})
export class UserProfileComponent implements OnInit {
    
    constructor(private userService: UserService, private adpqService: ADPQService, private router: Router) { }

    ngOnInit() {
        this.adpqService.breadcrumbItems = [{ label: 'User Home', routerLink: ['./user'] }, { label: 'User Profile', routerLink: ['./user/profile'] }];
    }

    async onSignupFormSubmit(vm: UserFormViewModel) {
        if (vm && vm.user) {
            vm.user.state = StatesFactory.getStates()[vm.selectedStateIdx].shortName;
            this.userService.updateUserInfo(vm.user);
            this.adpqService.growl({ severity: 'success', summary: `Profile successfully updated` });
            vm.user = new User();
        }
        else
            this.router.navigate(['./user']);
    }
}