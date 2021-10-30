import { Time } from '@angular/common';
import { Component, Injector, OnInit } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { FormDto, FormListDto, FormListDtoPagedResultDto, FormServiceProxy, UserDtoPagedResultDto } from '@shared/service-proxies/service-proxies';
import { finalize } from 'rxjs/operators';

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

    constructor(injector: Injector, private _services: FormServiceProxy) {
        super(injector);

    }

    ngOnInit() { }

    protected list(
        request: PagedApplicationRequestDto,
        pageNumber: number,
        finishedCallback: Function
    ): void {
        request.keyword = this.keyword;

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

