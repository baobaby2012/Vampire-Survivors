# 🗡️ [Tên Game Của Bạn] - 2D Survival Roguelite

![Unity](https://img.shields.io/badge/Unity-100000?style=for-the-badge&logo=unity&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![Status](https://img.shields.io/badge/Status-In%20Development-blueviolet?style=for-the-badge)

Một tựa game sinh tồn 2D Pixel Art mang phong cách auto-attack thời gian thực (tương tự Vampire Survivors). Người chơi sẽ phải đối mặt với hàng ngàn quái vật bao vây từ mọi phía, thu thập tinh thể kinh nghiệm, thăng cấp và cố gắng sống sót lâu nhất có thể trước khi thời gian kết thúc.

## 🌟 Tính năng nổi bật (Key Features)

* **Hệ thống sinh quái động (Dynamic Wave Spawner):** Quái vật xuất hiện xung quanh người chơi. Càng sống sót lâu, số lượng quái xuất hiện cùng lúc càng đông và thời gian giữa các đợt đẻ quái càng ngắn lại.
* **Độ khó tăng tiến theo thời gian (Time-Based Difficulty):** Tích hợp `DifficultyManager` tự động khuếch đại máu và tốc độ di chuyển của quái vật theo mỗi phút trôi qua.
* **Đường cong kinh nghiệm lũy tiến:** Sử dụng công thức toán học hàm mũ, yêu cầu lượng EXP khổng lồ ở giai đoạn cuối game để cân bằng sức mạnh người chơi.
* **Tối ưu hóa hiệu năng cực cao:** Sử dụng triệt để kiến trúc **Object Pooling** cho quái vật, tinh thể EXP và sát thương nảy (Floating Text), cho phép render hàng trăm Entity trên màn hình mà không bị tụt FPS hay rác bộ nhớ (Garbage Collection).
* **Đồ họa Pixel Art:** Hoạt ảnh mượt mà, phản hồi hình ảnh rõ ràng khi quái bị trúng đòn (Shader chớp trắng).

## 🛠️ Kiến trúc hệ thống & Kỹ thuật sử dụng

Dự án này được xây dựng với mục tiêu Code Clean và dễ dàng mở rộng, áp dụng các Design Pattern chuẩn mực trong phát triển Game với Unity:

* **Singleton Pattern:** Áp dụng cho các hệ thống cốt lõi chỉ có một bản thể duy nhất (`DifficultyManager`, `EnemySpawner`, `ObjectPoolManager`).
* **Object Pooling:** Tái sử dụng GameObject thay vì Instantiate/Destroy liên tục để tối ưu Memory.
* **Kế thừa & Đa hình (Inheritance & Polymorphism):** Xây dựng lớp cha `Character` chứa các thuộc tính chung (Máu, Initialize, ReceiveDamage), các loại quái đặc thù (`Enemy`) sẽ kế thừa và mở rộng tính năng.
* **Coroutines:** Xử lý các tác vụ bất đồng bộ và trì hoãn thời gian (Delay Spawn, Hit Animation, Stage Timer) hiệu quả, không chặn luồng chính.

## 🚀 Hướng dẫn cài đặt (Getting Started)

### Yêu cầu hệ thống
* **Unity Editor:** Khuyến nghị phiên bản `2022.3 LTS` hoặc mới hơn.
* **Visual Studio / Rider** (hoặc bất kỳ IDE nào hỗ trợ C#).

### Các bước khởi chạy
1. Clone kho lưu trữ này về máy:
   ```bash
   git clone [https://github.com/your-username/your-repo-name.git](https://github.com/your-username/your-repo-name.git)

   