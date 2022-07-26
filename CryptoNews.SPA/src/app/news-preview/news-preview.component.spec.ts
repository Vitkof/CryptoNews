import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewsPreviewComponent } from './news-preview.component';

describe('NewsPreviewComponent', () => {
  let component: NewsPreviewComponent;
  let fixture: ComponentFixture<NewsPreviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NewsPreviewComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NewsPreviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
