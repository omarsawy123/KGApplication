import { Time } from '@angular/common';
import { Component, Injector, OnInit, ViewChild } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { DateDropDownDto, FormDto, FormListDto, FormListDtoPagedResultDto, FormServiceProxy, TimeDropDownDto, UserDtoPagedResultDto } from '@shared/service-proxies/service-proxies';
import { BsModalService } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { ViewApplicationComponent } from './ViewApplication.component';

class PagedApplicationRequestDto extends PagedRequestDto {
    keyword: string;
    dateId: number;
    timeId: number;
}


@Component({
    selector: 'ApplicationList',
    templateUrl: 'ApplicationList.component.html',
    animations: [appModuleAnimation()]

})

export class ApplicationListComponent extends PagedListingComponentBase<FormDto> implements OnInit {

    forms: FormListDto[] = [];
    keyword = '';
    isActive: boolean | null;
    advancedFiltersVisible = false;
    dateId: number = 0;
    timeId: number = 0;
    dates_List: DateDropDownDto[] = [];
    times_List: TimeDropDownDto[] = [];


    constructor(injector: Injector, private _services: FormServiceProxy, private _modalServices: BsModalService) {
        super(injector);
        this._services.getAllDatesForDropDown().subscribe((result) => {
            this.dates_List = result;
        })

        this._services.getAllTimesForDropDown().subscribe((result) => {
            this.times_List = result;
        })
    }

    ngOnInit() {

        this.refresh();

    }

    viewForm(id: number) {

        this._services.getForm(id).subscribe((result) => {

            this._modalServices.show(ViewApplicationComponent, {
                class: 'modal-xl',
                initialState: {
                    form: result.form,
                    dateName: result.dateName,
                    timeName: result.timeName,
                    year: result.years,
                    month: result.months,
                    day: result.days,
                    printType: 1
                }
            })
        })
    }

    protected list(
        request: PagedApplicationRequestDto,
        pageNumber: number,
        finishedCallback: Function
    ): void {
        request.keyword = this.keyword;
        request.dateId = this.dateId;
        request.timeId = this.timeId;

        this._services
            .getAllForms(
                request.keyword,
                request.dateId,
                request.timeId,
                request.skipCount,
                request.maxResultCount
            )
            .pipe(
                finalize(() => {
                    finishedCallback();
                })
            )
            .subscribe((result: FormListDtoPagedResultDto) => {
                this.forms = result.items;
                this.showPaging(result, pageNumber);
            });
    }

    protected delete(form: FormDto): void {
        abp.message.confirm(
            this.l('Delete this Student Form?', form.studentName),
            undefined,
            (result: boolean) => {
                if (result) {
                    this._services.delete(form.id).subscribe(() => {
                        abp.notify.success(this.l('SuccessfullyDeleted'));
                        this.refresh();
                    });
                }
            }
        );
    }

}

