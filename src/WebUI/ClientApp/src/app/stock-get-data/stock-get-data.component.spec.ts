import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StockGetDataComponent } from './stock-get-data.component';

describe('StockGetDataComponent', () => {
  let component: StockGetDataComponent;
  let fixture: ComponentFixture<StockGetDataComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StockGetDataComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StockGetDataComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
