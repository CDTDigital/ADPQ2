﻿import { Component, OnInit, Input, Output, EventEmitter, ElementRef } from '@angular/core';

import { User } from './user.service';
import { StatesFactory } from '../shared/states';
import { SelectItem } from 'primeng/primeng';

export interface UserFormViewModel {
    user: User;
    selectedStateIdx: number;
}

export enum UserFormType {
    SIGNUP,
    SAVE
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
    userFormType: UserFormType;

    @Output()
    onSubmit: EventEmitter<UserFormViewModel> = new EventEmitter();

    states: SelectItem[] = StatesFactory.getStatesAsSelectItems();
    selectedStateIdx = 4;
    confPassword: string;
    isShowingPassfordFields = false;

    constructor(private el: ElementRef) { }

    ngOnInit() {
        this.el.nativeElement.querySelectorAll('[name="user-fname-input"]')[0].focus();
    }
}