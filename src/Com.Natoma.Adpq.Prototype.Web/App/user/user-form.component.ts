import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

import { User } from './user.service';
import { StatesFactory } from '../shared/states';
import { SelectItem } from 'primeng/primeng';


@Component({
    selector: 'adpq-user-form',
    templateUrl: '../../html/user-form.component.html',
    moduleId: module.id,
})
export class UserFormComponent implements OnInit {

    @Input()
    user: User;

    @Output()
    onSubmit: EventEmitter<{ user: User, selectedStateIdx: number }> = new EventEmitter();

    states: SelectItem[] = StatesFactory.getStatesAsSelectItems();
    selectedStateIdx = 4;
    isSignup: boolean;
    confPassword: string;

    ngOnInit() {
        if (!this.user.email)
            this.isSignup = true;
    }
}