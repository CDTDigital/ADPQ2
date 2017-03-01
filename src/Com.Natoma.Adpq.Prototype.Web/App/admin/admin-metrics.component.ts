import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { Notification, NotificationService, NotificationMetricsViewModel, DeliveryMethodEnum } from '../shared/notification.service';
import { UserService } from '../user/user.service';
import { ADPQService } from '../shared/adpq.service';
import { MenuItem } from 'primeng/primeng';

interface ChartData {
    labels: string[];
    datasets: ChartDataset[];
}

interface ChartDataset {
    label: string;
    data: number[];
    borderColor: string;
}

@Component({
    templateUrl: '../../html/admin-metrics.component.html',
    moduleId: module.id,
})
export class AdminMetricsComponent implements OnInit {
    metrics: NotificationMetricsViewModel[] = [];
    chartData: any;
    isChartDataReady: boolean;

    constructor(private userService: UserService, private adpqService: ADPQService, private router: Router,
        private notificationService: NotificationService) { }

    async ngOnInit() {
        if (!this.userService.checkLogin()) {
            this.router.navigate(["./login"]);
            return;
        }

        let user = await this.userService.getLoggedInUser();
        if (user && user.isAdmin == false)
            this.router.navigate(["./user"]);

        this.adpqService.breadcrumbItems = [<MenuItem>{ label: 'Admin Home', routerLink: ['./admin'] }, <MenuItem>{ label: 'Admin Metrics', routerLink: ['./admin/metrics'] }];

        this.metrics = await this.notificationService.getMetricsData();
        this.chartData = this.getChartData(this.metrics);
        this.isChartDataReady = true;
    }

    private getChartData(metrics: NotificationMetricsViewModel[]): ChartData {
        let labels: string[] = [];
        let datasets: ChartDataset[] = [];
        datasets.push({ label: "Email", data: [], borderColor: "#87CEFA" });
        datasets.push({ label: "SMS", data: [], borderColor: "#FF7F50" });

        for (let metric of metrics) {
            if (labels.indexOf(metric.dateSentDisplay) < 0)
                labels.push(metric.dateSentDisplay);

            if (metric.sendType == DeliveryMethodEnum.EMAIL)
                datasets[0].data.push(metric.count);
            else
                datasets[1].data.push(metric.count);
        }

        return <ChartData>{ datasets: datasets, labels: labels };
    }
}