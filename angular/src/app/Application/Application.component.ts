import { ChangeDetectionStrategy, Component, Injector, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, Validators } from '@angular/forms';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/app-component-base';
import { FormDto, FormServiceProxy } from '@shared/service-proxies/service-proxies';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker/bs-datepicker.config';


@Component({
    selector: 'AppReg',
    templateUrl: 'Application.component.html',
    animations: [appModuleAnimation()],
    changeDetection: ChangeDetectionStrategy.OnPush
})

export class ApplicationComponent extends AppComponentBase implements OnInit {


    model: FormDto = new FormDto();
    ApplicationForm: FormGroup;
    datePickerConfig: Partial<BsDatepickerConfig>;

    constructor(inject: Injector, private fb: FormBuilder, private _services: FormServiceProxy) {
        super(inject);

        this.datePickerConfig = Object.assign({}, {
            containerClass: 'theme-dark-blue',
            dateInputFormat: 'DD/MM/YYYY',
            isAnimated: true,
            isDisabled: true,
        });
    }



    ngOnInit() {

        this.ApplicationForm = this.fb.group({
            studentName: ['', Validators.required],
            studentNameAr: ['', Validators.required],
            studentBirthDate: ['', Validators.required],
            studentReligion: ['', Validators.required],
            fatherJob: ['', Validators.required],
            motherJob: ['', Validators.required],
            fatherMobile: ['', Validators.required],
            motherMobile: ['', Validators.required],
            telephone: ['', Validators.required],
            email: ['', Validators.required],
            studentNationalId: ['', Validators.required],
            studentRelativeName: [''],
            studentRelativeGrade: [''],
            joiningSchool: ['', Validators.required],
        })

    }


    save() {

        console.log(this.model);
        // this._services.create()

    }


}