import {
  Component,
  ChangeDetectionStrategy,
  OnInit,
  Injector
} from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import {
  UserServiceProxy,
  ChangeUserLanguageDto,
  FormServiceProxy
} from '@shared/service-proxies/service-proxies';
import { filter as _filter } from 'lodash-es';

@Component({
  selector: 'header-language-menu',
  templateUrl: './header-language-menu.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class HeaderLanguageMenuComponent extends AppComponentBase
  implements OnInit {
  languages: abp.localization.ILanguageInfo[];
  currentLanguage: abp.localization.ILanguageInfo;

  constructor(injector: Injector, private _userService: UserServiceProxy, private formService: FormServiceProxy) {
    super(injector);
  }

  ngOnInit() {

    debugger
    this.languages = _filter(
      this.localization.languages,
      (l) => !l.isDisabled
    );

    this.languages = this.languages.filter(l => l.displayName == "German" || l.displayName == "العربية")
    
    this.currentLanguage = this.localization.currentLanguage;


  }

  changeLanguage(languageName: string): void {
    const input = new ChangeUserLanguageDto();
    input.languageName = languageName;

    this.formService.changeLanguage(input).subscribe(() => {
      abp.utils.setCookieValue(
        'Abp.Localization.CultureName',
        languageName,
        new Date(new Date().getTime() + 5 * 365 * 86400000), // 5 year
        abp.appPath
      );

      window.location.reload();
    });
  }
}
