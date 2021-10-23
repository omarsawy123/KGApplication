import { Component, OnInit } from '@angular/core';
import { NavigationStart, Router } from '@angular/router';
import { FormDto, FormServiceProxy } from '@shared/service-proxies/service-proxies';
import { filter } from 'lodash';

@Component({
    selector: 'viewApp',
    templateUrl: 'ViewApplication.component.html'
})

export class ViewApplicationComponent implements OnInit {

    formId: number;
    form: FormDto;

    constructor(private router: Router, private _services: FormServiceProxy) {
        const nav = this.router.getCurrentNavigation();
        this.formId = nav.extras.state ? nav.extras.state.formId : 0;
        if (this.formId != 0) {
            this._services.getForm(this.formId).subscribe((result) => {
                this.form = result;
            });
            
        }
    }

    ngOnInit() {



    }
}