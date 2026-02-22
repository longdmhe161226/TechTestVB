# Hệ thống Bán Vé Nhà Hát (Theater Ticket System)

Ứng dụng WinForms VB.NET quản lý suất diễn, đặt vé và gán ghế cho nhà hát.

## Yêu cầu hệ thống

- **.NET 10.0 SDK** (hoặc mới hơn)
- **PostgreSQL** (phiên bản 12+)
- **NuGet Package**: Npgsql (đã cài sẵn trong project)

## Cách cài đặt và chạy

### 1. Thiết lập Database PostgreSQL

1. Tạo database mới tên `theater_db`:
   ```sql
   CREATE DATABASE theater_db;
   ```

2. Chạy file `database.sql` để tạo các bảng:
   ```bash
   psql -U postgres -d theater_db -f database.sql
   ```

### 2. Cấu hình kết nối

Mở file `TechTest/DbHelper.vb` và chỉnh sửa connection string nếu cần:

```vb
Private Const CONNECTION_STRING As String =
    "Host=localhost;Port=5432;Database=theater_db;Username=postgres;Password=postgres"
```

### 3. Chạy ứng dụng

```bash
dotnet run --project TechTest
```

Hoặc mở solution trong Visual Studio và nhấn F5.

## Chức năng
<img width="1919" height="1079" alt="image" src="https://github.com/user-attachments/assets/32f859c5-d444-42d2-84b8-a9ea942e763c" />

### 1. Quản lý Suất diễn (`frmPerformanceMaster`)
- Thêm / Sửa / Xóa suất diễn
- Tìm kiếm theo tên vở diễn và khoảng thời gian
- Hiển thị danh sách trong DataGridView
<img width="1919" height="1079" alt="image" src="https://github.com/user-attachments/assets/d919cda4-74a5-4f91-ba90-37784b60f856" />

### 2. Đặt Vé (`frmBooking`)
- Chọn suất diễn (có tìm kiếm)
- Nhập thông tin khách hàng, loại ghế, số lượng
- Tự động tính tổng tiền
- Hiển thị danh sách booking kèm ghế đã gán
<img width="1919" height="1079" alt="image" src="https://github.com/user-attachments/assets/dec22b02-e4f7-4da5-9943-a5249d9ae314" />

### 3. Gán Ghế (`frmSeatAssignment`)
- Sơ đồ ghế 10×10 (A–J × 1–10)
- Trạng thái ghế bằng màu: 🟥 Đã gán (booking khác) | 🟦 Đang chọn | ⬜ Trống
- Kiểm tra giới hạn số ghế theo vé đã đặt
- Kiểm tra trùng ghế giữa các booking
<img width="1919" height="1076" alt="image" src="https://github.com/user-attachments/assets/b81d257e-443e-4963-86c1-78f7a322834c" />

## Bảng giá

| Loại ghế | Giá (VND) |
|----------|-----------|
| Normal   | 100,000   |
| VIP      | 200,000   |
| Double   | 350,000   |

## Cấu trúc Database

- **performances**: Thông tin suất diễn (tên, thời gian, thời lượng)
- **bookings**: Thông tin đặt vé (suất diễn, khách hàng, loại ghế, số lượng, tổng tiền)
- **seat_assignments**: Gán ghế cụ thể cho booking (có ràng buộc UNIQUE để tránh trùng)

## Giả định và giới hạn

- Sơ đồ ghế cố định 10 hàng × 10 cột cho tất cả suất diễn
- Giá vé được quản lý trong bảng `seat_prices` (có thể thêm/sửa/bật/tắt loại ghế trực tiếp trong DB)
- Kết nối PostgreSQL chạy local (có thể thay đổi trong `DbHelper.vb`)



