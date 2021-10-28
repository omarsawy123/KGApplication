import { Component, Injector } from '@angular/core';
import { AbpSessionService } from 'abp-ng2-module';
import { AppComponentBase } from '@shared/app-component-base';
import { accountModuleAnimation } from '@shared/animations/routerTransition';
import { AppAuthService } from '@shared/auth/app-auth.service';
import { AccountServiceProxy } from '@shared/service-proxies/service-proxies';
import { ActivatedRoute } from '@angular/router';

@Component({
  templateUrl: './login.component.html',
  animations: [accountModuleAnimation()]
})
export class LoginComponent extends AppComponentBase {
  submitting = false;
  token: string;
  userId: number;
  userResult: import("c:/Users/Win 7/source/5.8.1/angular/src/shared/service-proxies/service-proxies").EmailConfirmationResult;
  paramValue: any;

  constructor(
    injector: Injector,
    public authService: AppAuthService,
    private _sessionService: AbpSessionService,
    private _service: AccountServiceProxy,
    private _route: ActivatedRoute
  ) {

    super(injector);

  }


  get multiTenancySideIsTeanant(): boolean {
    return this._sessionService.tenantId > 0;
  }

  get isSelfRegistrationAllowed(): boolean {
    if (!this._sessionService.tenantId) {
      return false;
    }

    return true;
  }

  login(): void {

    this._service.checkUserEmailConfirmation(this.authService.authenticateModel.userNameOrEmailAddress).subscribe((result) => {
      if (result) {
        this.submitting = true;
        this.authService.authenticate(() => (this.submitting = false));
      }
      else {
        abp.notify.error("Please Make sure to Register and Confirm Email First");
      }
    })

    // this.authService.authenticateModel.userNameOrEmailAddress

    // this._service.checkUserEmailConfirmation()


  }
}
