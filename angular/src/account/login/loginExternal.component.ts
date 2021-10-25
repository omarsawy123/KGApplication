import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AppAuthService } from '@shared/auth/app-auth.service';
import { AccountServiceProxy, EmailConfirmationResult } from '@shared/service-proxies/service-proxies';

@Component({
    selector: 'selector-name',
    templateUrl: 'loginExternal.component.html'
})

export class LoginExternalComponent implements OnInit {
    token: string;
    userId: number;
    userResult: EmailConfirmationResult;

    constructor(private _services: AccountServiceProxy, private _route: ActivatedRoute
        , public authService: AppAuthService,) {

        _route.paramMap.subscribe((map) => {
            this.token = map.get('tokenId')
            this.userId = +map.get('userId')

            this._services.confirmEmail(this.token, this.userId).subscribe((result) => {
                this.userResult = result;
                if (this.userResult.canLogin) {
                    authService.authenticateModel.userNameOrEmailAddress = this.userResult.userInfo.emailAddress
                    authService.authenticateModel.password = this.userResult.userInfo.password;

                    setTimeout(() => {
                        this.login();
                    }, 5000);
                }
            })

        })

    }

    login(): void {
        this.authService.authenticate();
    }

    ngOnInit() { }
}