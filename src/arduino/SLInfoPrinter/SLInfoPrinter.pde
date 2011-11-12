
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
  //ResetInfoFields();
}

void loop() {
  if(Serial.available()) {
    GetAvailableData();
  }else{
    Reset();
    PrintToScreen();
    delay(5000);
  }
}

void Reset(){
  currRow = 0;
  currCol = 0;
}

void PrintToScreen(){
  lcd.clear();
  int i=0;
  
  for(i=0;i<4;i++){
    lcd.setCursor(0,i);
  lcd.print(infofields[i]);
  }
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

