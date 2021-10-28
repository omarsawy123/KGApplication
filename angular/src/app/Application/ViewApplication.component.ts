import { ChangeDetectionStrategy, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { NavigationStart, Router } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
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

export class ViewApplicationComponent implements OnInit {

    formId: number;
    form: FormDto;
    dateName: string = "";
    timeName: string = "";
    loading: boolean = false;

    // @ViewChild('print') print:
    @ViewChild('pdfArabicData') pdfArabicData: ElementRef;
    @ViewChild('pdfHeader') pdfHeader: ElementRef;
    noApplication: boolean;



    constructor(private router: Router, private _services: FormServiceProxy) {

        this._services.checkUserApplication().subscribe((result) => {
            console.log(result);
            if (result != 0) {
                this._services.getForm(result).subscribe((frm) => {
                    this.form = frm.form;
                    this.dateName = frm.dateName;
                    this.timeName = frm.timeName;
                    console.log(frm);
                    this.ngOnInit();
                })
                // this.ViewApp = true;
                // this.router.navigate(['/app/viewapplication'], { state: { formId: result } })
            }
            else {
                this.noApplication = true;
            }

        })
        // const nav = this.router.getCurrentNavigation();
        // this.formId = nav.extras.state ? nav.extras.state.formId : 0;
        // if (this.formId != 0) {
        //     this._services.getForm(this.formId).subscribe((result) => {
        //         // this.form = result;
        //     });

        // }
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
                            margin: [0, 15, 0, 0]
                        },
                        {
                            text: 'Datum: ................',
                            margin: [0, 15, 0, 0]

                        },

                    ]
                },
                {
                    text: 'Prüfer: .............../...............',
                    margin: [0, 20, 0, 20]
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
                    margin: [0, 10, 0, 0]
                }
                ],
                pageMargins: [72, 72, 72, 100]//Left,Top,Right,Bottom
            };
            pdfMake.createPdf(docDefinition).open();
        })

    }

    ngOnInit() {



    }
}