﻿import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login.component';
import { UserHomeComponent } from './user/user-home.component';
import { AdminHomeComponent } from './admin/admin-home.component';
import { AdminNotificationComponent } from './admin/admin-notification.component';
import { AdminMetricsComponent } from './admin/admin-metrics.component';
import { UserProfileComponent } from './user/user-profile.component';
import { UserNotificationsComponent } from './user/user-notifications.component';
import { FaqComponent } from './faq.component';

const routes: Routes = [
    { path: "", redirectTo: "/user", pathMatch: "full" },
    { path: "home", redirectTo: "/user", pathMatch: "full" },

    { path: "login", component: LoginComponent },

    { path: "faq", component: FaqComponent },
    
    { path: "user/profile", component: UserProfileComponent },
    { path: "user/notifications", component: UserNotificationsComponent },
    { path: "user", component: UserHomeComponent },

    { path: "admin/profile", component: UserProfileComponent },
    { path: "admin/notification", component: AdminNotificationComponent },
    { path: "admin/metrics", component: AdminMetricsComponent },
    { path: "admin", component: AdminHomeComponent }
    
];
export const routing = RouterModule.forRoot(routes); 