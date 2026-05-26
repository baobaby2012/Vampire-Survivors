# 🦇 Vampire Survivors

Một tựa game sinh tồn 2D mang phong cách màn hình dọc/ngang với cơ chế Auto-attack và Roguelite, được lấy cảm hứng trực tiếp từ hiện tượng *Vampire Survivors*. Nhiệm vụ của người chơi là sống sót lâu nhất có thể giữa hàng ngàn quái vật bao vây, thu thập ngọc kinh nghiệm để nâng cấp vũ khí và khám phá những kỹ năng tối thượng.

---

## 🎓 Thông Tin Đề Tài

* **Trường:** Đại học Đà Lạt
* **Môn học:** Phát triển ứng dụng game
* **Giảng viên hướng dẫn:** Nguyễn Trọng Hiếu
* **Nền tảng phát triển:** Unity 2D Engine (C#)

### 🧑‍💻 Nhóm Phát Triển

| STT | Họ và Tên | Mã Số Sinh Viên | Vai trò |
| :-: | :--- | :--- | :--- |
| 1 | **Trương Bảo Bảo** | 2312582 | Core Gameplay, Weapon System, Object Pooling |
| 2 | **Hoàng Trịnh Việt Linh** | 2312664 | Enemy AI, UI/UX, Audio System, Spawner Logic |

---

## 🌟 Tính Năng Nổi Bật

* **Hệ thống Vũ Khí Đa Dạng:** Bao gồm Cây roi (Whip), Rìu (Axe), Sét đánh (Lightning), Đũa phép (Magic Wand),... kết hợp với các phụ kiện tăng cường chỉ số.
* **Object Pooling Tối Ưu:** Xử lý mượt mà hàng trăm kẻ địch và hàng ngàn viên ngọc kinh nghiệm (Crystal) trên màn hình cùng lúc mà không gây sụt giảm FPS.
* **Cơ Chế Level-Up Roguelite:** Lựa chọn nâng cấp vũ khí ngẫu nhiên mỗi khi lên cấp, tạo ra giá trị chơi lại (replayability) cao.
* **Đồ họa Pixel Art:** Giao diện hoài cổ kết hợp với hệ thống Particle System tạo hiệu ứng sát thương đã mắt.
* **Âm Thanh Sống Động:** Tích hợp đầy đủ nhạc nền BGM dồn dập và hiệu ứng âm thanh (SFX) cho từng vũ khí, tương tác UI và nhận sát thương.

---

## 🛠️ Kiến Trúc Khung (Tech Stack)

* **Ngôn ngữ:** C#
* **Game Engine:** Unity (Phiên bản khuyên dùng: 2022 LTS trở lên)
* **Design Pattern áp dụng:** Singleton (cho các trình quản lý như GameManager, Inventory), Object Pool (quản lý bộ nhớ).
* **UI System:** Unity Canvas, TextMeshPro.

---

## 📋 Phân Công Công Việc Chi Tiết

### Trương Bảo Bảo
- Xây dựng lớp kiến trúc cha (Character, Weapon, WeaponSpawner).
- Lập trình logic điều khiển nhân vật chính (Movement).
- Phát triển hệ thống Vũ khí & Phụ kiện (Inventory, Level up vũ khí).
- Tối ưu hóa hiệu suất với Object Pooling.

### Hoàng Trịnh Việt Linh
- Quản lý trí tuệ nhân tạo (AI) của kẻ địch và hệ thống sinh quái (Enemy Spawner).
- Thiết kế và lập trình luồng giao diện UI (Main Menu, Pause, Game Over, Level Up Window).
- Lập trình hệ thống Audio Manager (BGM, SFX).
- Xử lý các hiệu ứng hình ảnh (Damage Text, Screen Shake).

---

## 🚀 Hướng Dẫn Cài Đặt

1.  Clone kho lưu trữ này về máy:
    ```bash
    git clone [https://github.com/YourUsername/Pixel-Survivors.git](https://github.com/YourUsername/Pixel-Survivors.git)
    ```
2.  Mở **Unity Hub**, chọn **Add project from disk** và trỏ đến thư mục vừa clone.
3.  Mở Scene chính tại: `Assets/Scenes/MainGame.unity`.
4.  Nhấn nút **Play** trên cửa sổ Unity để trải nghiệm game.

---
*Dự án được thực hiện nhằm mục đích học tập và nghiên cứu trong khuôn khổ môn học Phát triển ứng dụng game.*