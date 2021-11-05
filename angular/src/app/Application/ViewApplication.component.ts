import { ChangeDetectionStrategy, ChangeDetectorRef, Component, ElementRef, Injector, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/app-component-base';
import { FormDto, FormServiceProxy } from '@shared/service-proxies/service-proxies';
import * as htmlToImage from 'html-to-image';
//PDF imports
import pdfMake from 'pdfmake/build/pdfmake';
import pdfFonts from 'pdfmake/build/vfs_fonts';//'pdfmake/build/vfs_fonts';
pdfMake.vfs = pdfFonts.pdfMake.vfs;



@Component({
    selector: 'viewApp',
    templateUrl: 'ViewApplication.component.html',
    styleUrls: ['ViewApplication.component.scss'],
    animations: [appModuleAnimation()],
    changeDetection: ChangeDetectionStrategy.OnPush

})

export class ViewApplicationComponent extends AppComponentBase implements OnInit {

    formId: number;
    form: FormDto;
    dateName: string = "";
    timeName: string = "";
    loading: boolean = false;
    printType: number = 0;
    year: number;
    month: number;
    day: number;

    currentLanguage: abp.localization.ILanguageInfo;


    @ViewChild('pdfArabicData') pdfArabicData: ElementRef;
    @ViewChild('pdfHeader') pdfHeader: ElementRef;
    noApplication: boolean = true;
    dayName: string;

    daysArabic = {
        Sunday: 'الأحد',
        Monday: 'الإثنين',
        Tuesday: 'الثلاثاء',
        Wednesday: 'الأربعاء',
        Thursday: 'الخميس',
        Friday: 'الجمعة',
        Saturday: 'السبت',

    }


    constructor(injector: Injector, private router: Router, private _services: FormServiceProxy, private cdRef: ChangeDetectorRef) {

        super(injector);

        this.currentLanguage = this.localization.currentLanguage;
    }

    ngOnInit() {

        this._services.checkUserApplication().subscribe((result) => {
            console.log(result);
            if (result != 0) {
                this.noApplication = false;
                this._services.getForm(result).subscribe((frm) => {
                    this.form = frm.form;
                    this.dateName = frm.dateName;
                    this.timeName = frm.timeName;
                    this.year = frm.years;
                    this.month = frm.months;
                    this.day = frm.days;
                    this.dayName = this.daysArabic[frm.dayName];

                    this.cdRef.detectChanges();
                })
            }
            else if (this.form != null) {
                this.noApplication = false;
                this.cdRef.detectChanges();
            }
        })

    }



    printStudentPdf() {

        htmlToImage.toPng(this.pdfArabicData.nativeElement).then(function (dataUrl) {

            var docDefinition = {
                content: [{
                    image: dataUrl,
                    width: 500,
                    margin: [0, -10, 0, 0],
                },
                ],
                pageMargins: [72, 72, 72, 100]//Left,Top,Right,Bottom
            };

            pdfMake.createPdf(docDefinition).open();
        })

    }



    printAllPdf() {


    }

    printPdf() {


        htmlToImage.toPng(this.pdfArabicData.nativeElement).then(function (dataUrl) {

            var docDefinition = {
                content: [{
                    image: dataUrl,
                    width: 500,
                    //height: 500,
                    margin: [0, -10, 0, 0],
                },
                {
                    canvas: [{ type: 'line', x1: 0, y1: 5, x2: 595 - 2 * 40, y2: 5, lineWidth: 1 }]
                },
                {
                    columns: [
                        {
                            text: 'Gruppe: ...............',
                            margin: [0, 10, 0, 0]
                        },
                        {
                            text: 'Datum: ................',
                            margin: [0, 10, 0, 0]

                        },

                    ]
                },
                {
                    text: 'Prüfer: .............../...............',
                    margin: [0, 10, 0, 20]
                },
                {
                    alignment: 'center',
                    table: {
                        body: [
                            ['Testaufgabe', 'Ergebnis'],
                            ['1', ''],
                            ['2', ''],
                            ['3', ''],
                            ['4', ''],
                            ['5', ''],
                            ['6', ''],
                            ['7', ''],
                            ['8', ''],
                            ['Punktzahl', '']
                        ]
                    }
                },
                {
                    text: 'Sonstige Beobachtungen',
                    margin: [0, 5, 0, 10]
                },
                {
                    canvas: [{ type: 'line', x1: 0, y1: 5, x2: 595 - 2 * 40, y2: 5, lineWidth: 1 }]
                },
                {
                    canvas: [{ type: 'line', x1: 0, y1: 5, x2: 595 - 2 * 40, y2: 5, lineWidth: 1 }],
                    margin: [0, 15, 0, 0]

                },
                ],
                pageMargins: [72, 72, 72, 100]//Left,Top,Right,Bottom
            };


            pdfMake.createPdf(docDefinition).open();
        })

    }


}