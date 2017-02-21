import {Component, OnInit} from '@angular/core';

import { Subscription }   from 'rxjs/Subscription';

@Component({
    selector: 'adpq-app',
    templateUrl: '../html/home.html'
})
export class AppComponent implements OnInit {
    
    ngOnInit() {
        console.log("in app component OnInit");
    }
}