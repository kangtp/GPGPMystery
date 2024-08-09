# 어둑시니 (2023 인디 게임 제작)
<div align="center">
<img width="237" alt="어둑시니로고" src="https://github.com/user-attachments/assets/b3909896-cea6-4a51-866f-77139ced4f9e">

한 어린 사냥꾼이 어둑시니를 잡기 위해 숲에 들어왔다가, 어둑시니의 슬픈 이야기를 듣게 된다. 이후, 사냥꾼은 어둑시니와 함께 숲을 탈출하기로 결심한다.
이 게임은 빛과 그림자를 이용하여 길을 만들어 나가는 퍼즐 게임이다. 플레이어는 꼬마 사냥꾼과 어둑시니를 조작하여, 숲을 탈출하는 데 필요한 길을 만들어야 한다.
</div>

### ▶️[게임 트레일러] (https://youtu.be/Au1-Q9dEQWc)

## 목차
  - [개요](#개요)
  - [게임 목표](#게임-목표)
  - [게임 설명](#게임-설명)
  - [게임 플레이 방식](#게임-플레이-방식)

## 개요
- 프로젝트 이름: 어둑시니
- 프로젝트 지속기간: 2023.09-2022.12
- 개발 엔진 및 언어: Unity & C#
- 멤버: 강현서,김태완,박선준
- 게임 장르: 어드벤처 게임🛹/ 퍼즐 게임🧩/ 2D 형태의 픽셀게임 🎮

## 게임 목표
- 빛과 그림자를 활용하여 어둑시니와 주인공인 꼬마 사냥꾼을 목적지까지 도달할 수 있도록 만드는 것이 목표이다. 꼬마 사냥꾼은 흰색 화살표, 어둑시니는 빨간색 화살표까지 도달해야 한다.
- 어둑시니는 어두운 길만 이동할 수 있고, 꼬마 사냥꾼은 밝은 길로만 이동이 가능하다. 맵에는 화로, 반딧불이, 도깨비불과 같이 빛을 내는 구조물들이 배치되어 있다. 통나무를 움직여 빛을 가리거나 가리지 않게 하여 그림자 길과 밝은 길을 만들 수 있다.
<div align="center">
<img width="400" alt="게임목표" src="https://github.com/user-attachments/assets/730adb30-ab12-48c6-88f5-811c2eba1698">
</div>

## 게임 플레이 방식
<어둑시니>는 꼬마사냥꾼과 어둑시니를 각자의 정해진 목적지까지 이동시키는 것입니다. 꼬마 사냥꾼은 그림자가 진 어두운 길을 이동할 수 없고, 반대로 어둑시니는 빛이 비추는 밝은 길을 이동할 수 없습니다. 

따라서, 밝은 길과 어두운 길을 함께 고려해 두 캐릭터가 같이 탈출할 수 있는 루트를 만들어야 합니다! 


|Home|Forest|Store|Room|
|---|---|---|---|
|![image](https://user-images.githubusercontent.com/66003567/216816017-bfd18669-9f70-45c2-8561-bae648690602.png)|![image](https://user-images.githubusercontent.com/66003567/216815971-d8ed6ea8-1f92-45f8-9611-1cbe2b5e8db0.png)|![image](https://user-images.githubusercontent.com/66003567/216815991-88e0f4d6-3e5d-4c19-9eb9-97047b40c0d0.png)|![image](https://user-images.githubusercontent.com/66003567/216816002-4eca6510-4436-44f5-b949-347e75129ada.png)|
|옥수수 농사 가능|슈팅, 두더지 잡기, 낚시 미니게임 입장 가능|물품 판매 및 구매 가능|게임 저장 가능|

- 미니게임

|베이킹|슈팅|두더지 잡기|낚시|
|---|---|---|---|
|![image](https://user-images.githubusercontent.com/66003567/216816081-cf4a29c6-72f3-4b75-b01d-3dd6e3faabc5.png)|![image](https://user-images.githubusercontent.com/66003567/216816088-cd83d20a-e023-4af2-b406-98197af5ff35.png)|![image](https://user-images.githubusercontent.com/66003567/216816106-5a97f26e-565b-43a6-bfab-d22e36745f80.png)|![image](https://user-images.githubusercontent.com/66003567/216816119-fb22c507-f6c4-49a3-b4f0-28ecaae94f6c.png)|
|오븐 타이머 조절 성공시 빵 획득|나무 몬스터 제거시 코인 획득|일정 수 이상 두더지 잡기 성공시 코인 획득|찌를 올바른 위치에 멈추기 3회 성공시 선택한 난이도의 물고기 획득|
