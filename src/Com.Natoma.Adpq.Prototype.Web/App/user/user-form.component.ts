import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

import { User } from './user.service';
import { StatesFactory } from '../shared/states';
import { SelectItem } from 'primeng/primeng';

export interface UserFormViewModel {
    user: User;
    selectedStateIdx: number;
}

@Component({
    selector: 'adpq-user-form',
    templateUrl: '../../html/user-form.component.html',
    moduleId: module.id,
})
export class UserFormComponent implements OnInit {

    @Input()
    user: User;

    @Input()
    submitLabel: string;

    @Output()
    onSubmit: EventEmitter<UserFormViewModel> = new EventEmitter();

    states: SelectItem[] = StatesFactory.getStatesAsSelectItems();
    selectedStateIdx = 4;
    confPassword: string;
    isShowingPassfordFields = false;

    ngOnInit() {
    }
}