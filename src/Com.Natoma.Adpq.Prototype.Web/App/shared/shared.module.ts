import { NgModule, ModuleWithProviders } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { AuthService } from './auth.service';
import { ADPQService } from './adpq.service';
import { UserService } from '../user/user.service';
import { NotificationService } from '../shared/notification.service';
import { CookieService } from 'angular2-cookie/core'; 

import {
    InputTextModule, DataTableModule, ButtonModule, DialogModule, PanelModule, DropdownModule, InputSwitchModule, InputTextareaModule, ProgressBarModule, TabViewModule,
    GrowlModule, InputMaskModule, MultiSelectModule, CheckboxModule, FileUploadModule, ConfirmDialogModule, ConfirmationService, ToggleButtonModule, TooltipModule, BlockUIModule,
    CalendarModule, MessagesModule, BreadcrumbModule, EditorModule, SharedModule as pngSharedModule
}
    from 'primeng/primeng';

@NgModule({
    imports: [
        CommonModule, HttpModule, FormsModule,
        InputTextModule, DataTableModule, ButtonModule, DialogModule, PanelModule, DropdownModule, InputSwitchModule, InputTextareaModule, ProgressBarModule, TabViewModule, GrowlModule, InputMaskModule,
        MultiSelectModule, CheckboxModule, FileUploadModule, ConfirmDialogModule, ToggleButtonModule, TooltipModule, BlockUIModule, CalendarModule, MessagesModule, BreadcrumbModule,
        EditorModule, pngSharedModule
    ],

    exports: [
        CommonModule, FormsModule, HttpModule,
        InputTextModule, DataTableModule, ButtonModule, DialogModule, PanelModule, DropdownModule, InputSwitchModule, InputTextareaModule, ProgressBarModule, GrowlModule, InputMaskModule, TabViewModule,
        MultiSelectModule, CheckboxModule, FileUploadModule, ConfirmDialogModule, ToggleButtonModule, TooltipModule, BlockUIModule, BreadcrumbModule,
        CalendarModule, MessagesModule, EditorModule, pngSharedModule
    ]
})
export class SharedModule {
    static forRoot(): ModuleWithProviders {
        return {
            ngModule: SharedModule,
            providers: [AuthService, ADPQService, UserService, CookieService, NotificationService]
        };
    }
}