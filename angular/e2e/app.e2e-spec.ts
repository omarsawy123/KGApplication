import { TestTemplatePage } from './app.po';

describe('Test App', function() {
  let page: TestTemplatePage;

  beforeEach(() => {
    page = new TestTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
