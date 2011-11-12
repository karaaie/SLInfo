
// include the library code:
#include <LiquidCrystal.h>

// initialize the library with the numbers of the interface pins
LiquidCrystal lcd(7, 8, 9, 10, 11, 12);

char infofields[4][20];


int currRow=1,currCol=0;
void setup() {
  // set up the LCD's number of columns and rows: 
  lcd.begin(20, 4);
  Serial.begin(9600);
}

void loop() {
  if(Serial.available()) {
    GetAvailableData();
  }else{
    Reset();
    PrintToScreen();
    delay(1000);
  }
}

void Reset(){
  currRow = 0;
  currCol = 0;
}

void PrintToScreen(){
  lcd.clear();
  lcd.setCursor(0,0);
  lcd.print(infofields[0]);
  lcd.setCursor(0,0);  
  lcd.print(infofields[1]);
  lcd.setCursor(0,2);
  lcd.print(infofields[2]);
  lcd.setCursor(0,3);
  lcd.print(infofields[3]);
}

void GetAvailableData(){
  char ch = Serial.read();

  if(ch == 'X'){ 
    currRow++;
    currCol = 0;
  }else{
    infofields[currRow][currCol] = ch;
    currCol++;
  }
}

