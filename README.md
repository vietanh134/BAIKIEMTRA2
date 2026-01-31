README – HỆ THỐNG QUẢN LÝ CLB PICKLEBALL
"VỢT THỦ PHỐ NÚI" (PCM)

📌 Thông tin chung
Môn học: Lập trình Web với ASP.NET Core (Razor Pages)

Bài kiểm tra: BÀI KIỂM TRA 02

Công nghệ sử dụng:
ASP.NET Core Razor Pages
Entity Framework Core (Code First + Migration)
ASP.NET Core Identity
SQL Server
Tên đề tài: Hệ thống quản lý CLB Pickleball Vợt Thủ Phố Núi (PCM)

🎯 Mục tiêu hệ thống
Hệ thống PCM được xây dựng nhằm hỗ trợ CLB Pickleball Vợt Thủ Phố Núi quản lý toàn diện các hoạt động của câu lạc bộ trên nền tảng web, bao gồm:
Quản lý Hội viên và trình độ thi đấu (Rank DUPR)
Quản lý Tài chính thu/chi minh bạch, theo dõi quỹ theo thời gian thực
Hỗ trợ Đặt sân, thi đấu giao hữu, kèo thách đấu và mini-game
Áp dụng phân quyền – bảo mật rõ ràng theo vai trò
Nâng cao trải nghiệm người dùng, dễ sử dụng và mở rộng
👉 Phương châm hoạt động: “Vui – Khỏe – Có Thưởng”

🧩 Phân hệ chính

1️⃣ Quản trị nội bộ (Operations)
Quản lý Hội viên (Members), liên kết trực tiếp với Identity User
Quản lý Tin tức & Thông báo (News, Notifications)
Quản lý Tài chính (Treasury):
Danh mục Thu/Chi
Giao dịch tài chính
Cảnh báo quỹ âm theo thời gian thực

2️⃣ Hoạt động thường nhật
Quản lý Sân thi đấu (Courts)
Quản lý Đặt sân (Booking):
Chặn trùng lịch cùng sân
Kiểm tra thời gian hợp lệ
Giới hạn số slot đặt sân mỗi ngày

3️⃣ Thi đấu & Sự kiện (Sports Core)
Quản lý Trận đấu giao hữu (Singles / Doubles)
Quản lý Kèo thách đấu (Duel / Challenge)
Tổ chức Mini-game:
Team Battle
Round Robin
Tự động cập nhật Rank DUPR khi trận đấu được đánh dấu IsRanked

4️⃣ ⚖️ Trọng tài & Ghi nhận kết quả
Chỉ Referee hoặc Admin được phép nhập kết quả trận đấu
Kiểm tra đội hình hợp lệ (không trùng người chơi)
Tự động xử lý sau khi trận đấu kết thúc:
Cập nhật Rank
Cập nhật điểm Team Battle
Kết thúc Challenge khi đủ điều kiện
Phân phối thưởng

5️⃣ 🔐 Phân quyền & Bảo mật
Admin: Toàn quyền hệ thống
Treasurer: Quản lý tài chính
Referee: Ghi nhận và xác nhận kết quả trận đấu
Member: Người dùng thông thường
Áp dụng Role-based Authorization cho Razor Pages

🗄️ Thiết kế Cơ sở dữ liệu
Sử dụng Entity Framework Core – Code First + Migration
Quy ước: Tên bảng nghiệp vụ bắt đầu bằng 3 số cuối MSSV (044_)
Các nhóm bảng chính:
Members, News, Transactions, Categories
Courts, Bookings
Challenges, Matches, Participants
MatchScores, Notifications, ActivityLogs

👉 Thiết kế đảm bảo chuẩn hóa, liên kết chặt chẽ và đáp ứng đầy đủ nghiệp vụ thi đấu Đơn / Đôi / Challenge.
⚙️ Data Seeding (Bắt buộc)
Khi chạy Update-Database, hệ thống tự động tạo dữ liệu mẫu:
Identity:
1 Admin (admin@pcm.com)

6–8 Member mẫu

Courts:

Ít nhất 2 sân thi đấu

Tài chính:
Danh mục Thu/Chi mẫu
Giao dịch đảm bảo quỹ ban đầu dương

Hoạt động:
1 Mini-game Team Battle đang diễn ra
Participants được chia Team A / Team B
2–3 trận đấu đã có kết quả

🖥️ Giao diện & Trải nghiệm (UI/UX)
Xây dựng bằng Razor Pages + Bootstrap
Dashboard hiển thị:
Số dư quỹ
Số kèo đang mở

Top 5 Ranking
Calendar Booking theo tuần/tháng
Card UI cho Challenges
Toast notification, loading spinner
Responsive, thân thiện với thiết bị di động

🌟 Tính năng nâng cao (Bonus)
Xuất báo cáo tài chính (Excel / PDF)
Top Ranking Widget có avatar & xu hướng
Validate tuổi hội viên
Công thức tính Rank DUPR / Elo nâng cao
Notification & Activity Logs
Tìm kiếm & lọc dữ liệu nâng cao

🚀 Hướng dẫn chạy dự án
Clone project
Cấu hình appsettings.json (ConnectionString)
Chạy Migration:

cd PCMSystem:

dotnet restore:

dotnet ef database update:

dotnet run:


Run project và đăng nhập bằng:

Admin:  Admin@example.com
pass :  Vanh2006!

📎 Ghi chú
Rank của Member không được sửa thủ công
Rank chỉ được cập nhật thông qua Match
Các Match thuộc Challenge đã kết thúc sẽ bị khóa chỉnh sửa
✨ Dự án được xây dựng với mục tiêu học tập, mô phỏng nghiệp vụ thực tế và sẵn sàng mở rộng trong tương lai.
