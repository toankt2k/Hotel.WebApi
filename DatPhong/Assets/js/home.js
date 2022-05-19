$(document).ready(function(){
    loadData()
    
})
function loadData(){
    fetch('https://localhost:7268/api/v1/Rooms/EmptyRoom', {
                method: 'Get', // or 'PUT'
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${window.localStorage.getItem("token")}`
                },
            })
            .then(response => response.json())
            .then(data => {
                console.log(data)
                var roomContainer = document.querySelector('#roomContainer')
                roomContainer.innerHTML = ''
                var i = 1;
                for (var item of data) {
                    console.log(item.RoomImg)

                    let row = `
                    <div class="col-xl-4 col-lg-12 col-md-12 col-sm-12">
                        <div class="card shadow mb-4">
                            <!-- Card Header - Dropdown -->
                            <div class="card-header py-3 d-flex flex-row align-items-center justify-content-between bg-white">
                                <div class="name__page">
                                    <h4 class="name__page-tilte">
                                        Phòng ${item.RoomName == null ? "" : item.RoomName}
                                    </h4>
                                </div>
                                
                            </div>
                            <!-- Card Body -->
                            <div class="card-body">
                                <img src="${item.RoomImg}" alt="" width="100%">
                            </div>
                            <div class="room-description">
                                <span>${item.RoomDescription}</span>
                            </div>
                            <div class="room-price">
                                <span>${parseInt(item.RoomPrice)} VNĐ</span>
                            </div>
                            <div class="btn-container form-btn margin-none">
                                <button onclick="submit()" class="btn btn-primary btn-submit btn-custom">
                                    Đặt phòng
                                </button>
                            </div>
                        </div>
                        
                    </div>`
                    i++
                    $('#roomContainer').append(row)
                }
            })
            .catch((error) => {
                console.error('Error:', error);
            });
}