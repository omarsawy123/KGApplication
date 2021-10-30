import { ChangeDetectionStrategy, Component, ElementRef, Injector, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/app-component-base';
import { DatesDto, FormDto, FormServiceProxy, TimeTableDto } from '@shared/service-proxies/service-proxies';
import { result } from 'lodash';
import * as moment from 'moment';
import { DatepickerDateCustomClasses } from 'ngx-bootstrap/datepicker';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker/bs-datepicker.config';
import { TimepickerConfig } from 'ngx-bootstrap/timepicker';
import pdfMake from "pdfmake/build/pdfmake";
import pdfFonts from "pdfmake/build/vfs_fonts";

pdfMake.vfs = pdfFonts.pdfMake.vfs;
pdfMake.fonts = {
    arabicFont: {
        normal: 'https://cdn.jsdelivr.net/gh/rastikerdar/vazir-font@v27.2.2/dist/Vazir-Regular.ttf',
        bold: 'https://cdn.jsdelivr.net/gh/rastikerdar/vazir-font@v27.2.2/dist/Vazir-Regular.ttf',
        // italics: '../../assests/fonts/Almarai-Regular.ttf',
        // bolditalics: '../../assests/fonts/Almarai-Regular.ttf'
    },
    Roboto: {
        normal: 'https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.66/fonts/Roboto/Roboto-Regular.ttf',
        bold: 'https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.66/fonts/Roboto/Roboto-Medium.ttf',
        italics: 'https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.66/fonts/Roboto/Roboto-Italic.ttf',
        bolditalics: 'https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.66/fonts/Roboto/Roboto-MediumItalic.ttf'
    },
}


// export function getTimepickerConfig(): TimepickerConfig {


//     return Object.assign(new TimepickerConfig(), {
//         hourStep: 1,
//         minuteStep: 5,
//         showMeridian: false,
//         readonlyInput: false,
//         mousewheel: true,
//         showMinutes: true,
//         showSeconds: false,
//         labelHours: 'Hours',
//         labelMinutes: 'Minutes',
//         labelSeconds: 'Seconds',
//         allowEmptyTime: true,
//     });
// }

@Component({
    selector: 'AppReg',
    templateUrl: 'Application.component.html',
    animations: [appModuleAnimation()],
    styleUrls: ['Application.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush
})

export class ApplicationComponent extends AppComponentBase implements OnInit {

    model: FormDto = new FormDto();
    ApplicationForm: FormGroup;
    datePickerConfig: Partial<BsDatepickerConfig>;
    timePickerConfig: Partial<TimepickerConfig>;


    chooseDate: boolean = false;
    time;
    minTime: Date = new Date();
    maxTime: Date = new Date();
    bsInlineValue;
    dateCustomClasses: DatepickerDateCustomClasses[] = [];

    enabledDates = [];
    Dates: DatesDto[] = [];
    loading: boolean = true;
    timetable: TimeTableDto[] = [];
    r: FormDto;
    dateId: number;
    ViewApp: boolean = false;
    formView: FormDto;
    dateName: string;
    timeName: string;
    formId: number;
    invalidDate: boolean = false;
    birthDateValid: boolean = true;
    birthDateMinMaxInfo: string;
    saving: boolean;
    // enabledDates = [
    //     new Date('2021-10-20'),
    //     new Date('2021-10-22'),
    //     new Date('2021-10-21'),
    // ];


    constructor(inject: Injector, private fb: FormBuilder, private _services: FormServiceProxy, private router: Router) {
        super(inject);

        let d = new Date();
        var diff = d.getTimezoneOffset() * -1;


        this._services.checkApplicationStartEndDate(moment(d).add(diff, "minutes")).subscribe((result) => {
            if (!result) {
                this.invalidDate = true;
            }
        })

        this._services.getStudentBirthDateMinMax().subscribe((result) => {
            this.birthDateMinMaxInfo = result;
        })

        this.datePickerConfig = Object.assign({}, {
            containerClass: 'theme-dark-blue',
            dateInputFormat: 'DD/MM/YYYY',
            isAnimated: true,
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
            email: ['', [Validators.required, Validators.email]],
            hasRelative: [false],
            motherSchoolGraduate: [false],
            motherGradYear: [''],
            studentNationalId: ['', [Validators.required, Validators.minLength(14), Validators.maxLength(14)]],
            studentRelativeName: [''],
            studentRelativeGrade: [''],
            joiningSchool: ['', Validators.required],
            formTime: ['', Validators.required],
            formDate: ['', Validators.required],
        })

        this._services.getAllDates().subscribe((result) => {
            this.Dates = result;
            this.Dates.forEach((dt) => {
                if (dt.isEnabled) this.enabledDates.push(dt.dateValue.toDate())

            })
            // this.bsInlineValue = this.Dates[0].dateValue.toDate();
            // this.dateChanged(this.bsInlineValue);
            this.enabledDates.forEach((dt) => {
                this.dateCustomClasses.push({
                    date: dt,
                    classes: ['bg-primary']
                })
            })
        })

        this._services.checkUserApplication().subscribe((result) => {
            if (result != 0) {

                this.ViewApp = true;

                this._services.getForm(result).subscribe((frm) => {

                    this.ViewApp = true;

                    this.formView = frm.form
                    this.dateName = frm.dateName
                    this.timeName = frm.timeName

                    this.setViewValues(this.formView);
                })
            }

        })



    }

    setViewValues(formView: FormDto) {

        this.ApplicationForm.get('studentName').setValue(formView.studentName);
        this.ApplicationForm.get('studentNameAr').setValue(formView.studentNameAr);
        this.ApplicationForm.get('studentBirthDate').setValue(formView.studentBirthDate.toDate());
        this.ApplicationForm.get('studentReligion').setValue(formView.studentReligion);
        this.ApplicationForm.get('fatherJob').setValue(formView.fatherJob);
        this.ApplicationForm.get('motherJob').setValue(formView.motherJob);
        this.ApplicationForm.get('fatherMobile').setValue(formView.fatherMobile);
        this.ApplicationForm.get('motherMobile').setValue(formView.motherMobile);
        this.ApplicationForm.get('telephone').setValue(formView.telephone);
        this.ApplicationForm.get('email').setValue(formView.email);
        this.ApplicationForm.get('hasRelative').setValue(formView.hasRelatives);
        this.ApplicationForm.get('motherSchoolGraduate').setValue(formView.motherSchoolGraduate);
        this.ApplicationForm.get('motherGradYear').setValue(formView.motherGraduationYear);
        this.ApplicationForm.get('studentNationalId').setValue(formView.studentNationalId);
        this.ApplicationForm.get('studentRelativeName').setValue(formView.studentRelativeName);
        this.ApplicationForm.get('joiningSchool').setValue(formView.joiningSchool);

        this.formId = formView.id;

        let d = this.Dates.find(d => d.id == formView.dateId);
        this.dateId = formView.dateId;
        this.timetable = d.timeTable;

        this.ApplicationForm.get('formDate').setValue(formView.dateId);
        this.ApplicationForm.get('formTime').setValue(formView.timeId);


        this.checkValidation();



    }

    get f() {
        return this.ApplicationForm.controls;
    }



    checkPattern(event, alphaOnly?: boolean, noNegative?: boolean) {
        var k;
        k = event.charCode;

        if (alphaOnly) {
            return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 8 || k == 32);
        }

        if (noNegative) {
            return (k >= 48 && k <= 57);
        }
        return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 8 || k == 32 || (k >= 48 && k <= 57));
    }

    public arabicOnly(event) {
        var arabicCharUnicodeRange = /[\u0600-\u06FF]/;
        var str = String.fromCharCode(event.which);

        return (
            (event.charCode > 47 && event.charCode < 58) ||
            arabicCharUnicodeRange.test(str) ||
            event.charCode == 32
        )
    }



    dateChanged(val: Date) {


        if (val) {


            // get date value
            let d = this.Dates.find(d => d.dateValue.toDate().toLocaleDateString() == val.toLocaleDateString())


            // change selected date color
            this.dateCustomClasses.forEach((dt) => {
                if (dt.date.toLocaleDateString() == val.toLocaleDateString()) {
                    dt.classes = ['bg-sucess']
                }
                else {
                    dt.classes = ['bg-primary']
                }
            })

            // assign date and time values
            if (d) {

                this.timetable = d.timeTable;
                this.dateId = d.id;
                this.dateName = d.dateName;
                this.timeName = "";
                this.ApplicationForm.get('formTime').setValue('');
                this.ApplicationForm.get('formDate').setValue(d.id);




            }

        }


    }

    timeChanged(value: number) {

        this.timeName = this.timetable.find(t => t.id == value).timeName;

    }


    checkValidation() {

        if (this.f.hasRelative.value) {

            this.f.studentRelativeName.setValidators(Validators.required);
            this.f.studentRelativeName.updateValueAndValidity();

        }
        else if (!this.f.hasRelative.value) {

            this.f.studentRelativeName.clearValidators();
            this.f.studentRelativeName.updateValueAndValidity();

        }

        if (this.f.motherSchoolGraduate.value) {

            this.f.motherGradYear.setValidators(Validators.required);
            this.f.motherGradYear.updateValueAndValidity();

        }
        else if (!this.f.motherSchoolGraduate.value) {

            this.f.motherGradYear.clearValidators();
            this.f.motherGradYear.updateValueAndValidity();

        }

    }



    getAgeAndBirthDate() {

        let val_NationalID =
            this.ApplicationForm.get("studentNationalId").value.toString();


        let Ids_list = [
            val_NationalID,

        ];

        for (let index = 0; index < Ids_list.length; index++) {
            let current_val = Ids_list[index];
            let year = 0;
            let month = 0;
            let day = 0;

            if (current_val.length == 14) {
                if (current_val[0] == "2") {
                    year = 1900;
                }
                if (current_val[0] == "3") {
                    year = 2000;
                }

                year += 10 * Number(current_val[1]) + Number(current_val[2]);

                month = 10 * Number(current_val[3]) + Number(current_val[4]);

                day = 10 * Number(current_val[5]) + Number(current_val[6]);

                if (index == 0) {


                    let d = new Date(year, month - 1, day);

                    console.log(d.toLocaleDateString());
                    this._services.checkStudentBirthDate(moment(d)).subscribe((result) => {
                        if (result) {

                            this.birthDateValid = true;
                            this.ApplicationForm.get("studentBirthDate").setValue(
                                new Date(year, month - 1, day)
                            );
                        }
                        else {
                            abp.message.error(this.birthDateMinMaxInfo);
                            this.birthDateValid = false;
                        }
                    })



                    // let val_age = new Date().getFullYear() - year;

                    // if (val_age < 0) {
                    //     val_age = 0;
                    //     this.ApplicationForm.get("studentBirthDate").setValue("");
                    // }

                    //   this.Student_Form.get("studentAge").setValue(val_age);

                    //   let gender = current_val[12] % 2 == 0 ? 2 : 1;
                    //   this.Student_Form.get("studentGender").setValue(gender);
                }
            }
        }
    }

    save() {

        var d = new Date();
        var diff = (d.getTimezoneOffset() * -1);

        this.model.studentName = this.ApplicationForm.controls.studentName.value;
        this.model.studentNameAr = this.ApplicationForm.controls.studentNameAr.value;
        this.model.studentNationalId = +this.ApplicationForm.controls.studentNationalId.value;
        this.model.studentBirthDate = moment(this.ApplicationForm.controls.studentBirthDate.value).add(diff, 'minutes');
        this.model.studentReligion = this.ApplicationForm.controls.studentReligion.value;
        this.model.fatherMobile = this.ApplicationForm.controls.fatherMobile.value;
        this.model.motherMobile = this.ApplicationForm.controls.motherMobile.value;
        this.model.fatherJob = this.ApplicationForm.controls.fatherJob.value;
        this.model.motherJob = this.ApplicationForm.controls.motherJob.value;
        this.model.telephone = this.ApplicationForm.controls.telephone.value;
        this.model.email = this.ApplicationForm.controls.email.value;
        this.model.hasRelatives = this.ApplicationForm.controls.hasRelative.value;
        this.model.motherSchoolGraduate = this.ApplicationForm.controls.motherSchoolGraduate.value;
        this.model.motherGraduationYear = this.ApplicationForm.controls.motherGradYear.value;
        this.model.studentRelativeName = this.ApplicationForm.controls.studentRelativeName.value;
        this.model.joiningSchool = this.ApplicationForm.controls.joiningSchool.value;
        this.model.studentRelativeGrade = "";
        this.model.dateId = this.dateId;
        this.model.timeId = this.ApplicationForm.controls.formTime.value;

        this.model.tenantId = 1;

        console.log(this.model);

        this.saving = true;


        if (this.formId) {
            this.model.id = this.formId;
            this._services.updateForm(this.model).subscribe(() => {
                abp.message.success("Application Updated !");
                setTimeout(() => {
                    window.location.reload();
                }, 2000);
            })
        }
        else {
            this._services.createForm(this.model).subscribe(() => {
                abp.message.success("Application Created !");
                setTimeout(() => {
                    window.location.reload();
                }, 2000);
            })
        }


    }


}