


const int trigPin1 = 53;
const int echoPin1 = 52;
const int trigPin2 = 39;
const int echoPin2 = 38;
const int vibPin1 = 45;
const int vibPin2 = 25;

void setup() {
  // initialize serial communication:
  Serial.begin(9600);
}

void loop()
{
  // establish variables for duration of the ping, 
  // and the distance result in inches
  long duration1, inch1, d2, inch2;

  // The sensor is triggered by a HIGH pulse of 10 or more microseconds.
  // Give a short LOW pulse beforehand to ensure a clean HIGH pulse:
  //set1
  sendSignal(trigPin1);
  //set2
  sendSignal(trigPin2);
  
  // Read the signal from the sensor: a HIGH pulse whose
  // duration is the time (in microseconds) from the sending
  // of the ping to the reception of its echo off of an object.
  //set1
  pinMode(echoPin1, INPUT);
  duration1 = pulseIn(echoPin1, HIGH);
  //set2
  pinMode(echoPin2, INPUT);
  d2 = pulseIn(echoPin2, HIGH);
  
  // convert the time into a distance
  inch1 = microsecondsToInches(duration1);
  inch2 = microsecondsToInches(d2);

  //Serial.print("Set 1: ");
  printOutputs(inch1);

  String grab = "";
  grab = Serial.read();
  //Parse(grab);
  
  incoming(inch1, vibPin1);

  //Serial.print("Set 2: ");
  printOutputs(inch2);
  
  
  Serial.print("/n"); Serial.println(" Got " + grab);
  
  delay(250);
}

void sendSignal(const int output){
  pinMode(output, OUTPUT);
  digitalWrite(output, LOW);
  delayMicroseconds(2);
  digitalWrite(output, HIGH);
  delayMicroseconds(10);
  digitalWrite(output, LOW);
}

void printOutputs(long inch){
  Serial.print(inch);
  Serial.print(" ");
}

//activates vibration motor
void incoming(long input, const int vib){
  long far = 42;
  long mid = 24;
  long clo = 12;
  if((input <= far) && (input > mid)){
    analogWrite(vib, 53);
  }

  else if((input <= mid) && (input > clo)){
    analogWrite(vib, 106);
  }

  else if((input <=clo) && (input > 1)){
    analogWrite(vib, 153);
  }
  else if(input <= 1){
    analogWrite(vib, 0);
  }
  else{
    analogWrite(vib, 0);
  }
}

long microsecondsToInches(long microseconds)
{
  // According to Parallax's datasheet for the PING))), there are
  // 73.746 microseconds per inch (i.e. sound travels at 1130 feet per
  // second).  This gives the distance travelled by the ping, outbound
  // and return, so we divide by 2 to get the distance of the obstacle.
  // See: http://www.parallax.com/dl/docs/prod/acc/28015-PING-v1.3.pdf
  return microseconds / 74 / 2;
}
