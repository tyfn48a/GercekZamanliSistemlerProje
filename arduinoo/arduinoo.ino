#include <LiquidCrystal.h>


LiquidCrystal lcd(8, 9, 4, 5, 6, 7);    
int trigPin = 24; /* Sensorun trig pini Arduinonun 6 numaralı ayağına bağlandı */
int echoPin = 26;  /* Sensorun echo pini Arduinonun 7 numaralı ayağına bağlandı */

const int Sensoroutpin = A9; //ses

int LDR_Pin = A8; //analog pin 0
int buzzerpin = 30;

int vibr_Pin =32; //titreşim

int deger;

const int esik = 200;  

long sure;
long uzaklik;


void setup() {

  pinMode(trigPin, OUTPUT); /* trig pini çıkış olarak ayarlandı */
  pinMode(echoPin, INPUT); /* echo pini giriş olarak ayarlandı */
  lcd.begin(16, 2);   
  lcd.setCursor(0, 0);


  pinMode(vibr_Pin, INPUT);
  
  Serial.begin(9600);
  pinMode(buzzerpin, OUTPUT);
}
void loop() {

  digitalWrite(trigPin, LOW); /* sensör pasif hale getirildi */
  delayMicroseconds(5);
  digitalWrite(trigPin, HIGH); /* Sensore ses dalgasının üretmesi için emir verildi */
  delayMicroseconds(10);
  digitalWrite(trigPin, LOW);  /* Yeni dalgaların üretilmemesi için trig pini LOW konumuna getirildi */
  sure = pulseIn(echoPin, HIGH); /* ses dalgasının geri dönmesi için geçen sure ölçülüyor */
  uzaklik = sure / 29.1 / 2; /* ölçülen sure uzaklığa çevriliyor */

  
  int LDRReading = analogRead(LDR_Pin);
  long measurement =TP_init();
 // Serial.print("measurment = ");
  //Serial.println(measurement);
Serial.print(uzaklik);
  Serial.print("/");
  Serial.print(deger);
  Serial.print("/");
  Serial.println(measurement);
//Serial.println(LDRReading);
  if (LDRReading < 50)
  {
    deger = analogRead(Sensoroutpin);
    lcd.print("--");
    //Serial.print(deger); /* hesaplanan uzaklık bilgisayara aktarılıyor */
    //Serial.println(" olarak olculmustur.");

    if (deger > esik)                        //ses algılanırsa
    {
      lcd.print("ses");
      
      tone(buzzerpin, 1000); // 1KHz ses sinyali gönderiliyor
      delay(250);
    }
    lcd.clear();

    int LDRReading = analogRead(LDR_Pin);
    long measurement =TP_init();
    // Serial.print("measurment = ");
    //Serial.println(measurement);             //titreşim algılanırsa
    if (measurement > 1000){
      lcd.print("hareket");
      
      tone(buzzerpin, 1000);
      delay(250);
    }
    lcd.clear();


    if(uzaklik <10){                         //mesafe az olursa
      lcd.print("yakinlik");
      
      tone(buzzerpin, 1000);
      delay(250);
    }
    lcd.clear();


      noTone(buzzerpin); // ses sinyalini durdur

  }
  else
    noTone(buzzerpin); // ses sinyalini durdur


  
  //delay(250); //Değerleri serial monitörden daha kolay okuyabilmeniz için konuldu kaldırılabilir...
}

long TP_init(){            //titreşim sensörü 
  delay(10);
  long measurement=pulseIn (vibr_Pin, HIGH);  //wait for the pin to get HIGH and returns measurement
  return measurement;
}

