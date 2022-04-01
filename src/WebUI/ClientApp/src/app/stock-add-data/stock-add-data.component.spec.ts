import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StockAddDataComponent } from './stock-add-data.component';

describe('StockAddDataComponent', () => {
  let component: StockAddDataComponent;
  let fixture: ComponentFixture<StockAddDataComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StockAddDataComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StockAddDataComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
