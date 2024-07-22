using AssignmentPRN1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AssignmentPRN1
{
    /// <summary>
    /// Interaction logic for CustomerBooking.xaml
    /// </summary>
    public partial class CustomerBooking : Window
    {
        private int customerId;
        public CustomerBooking(int customerId)
        {
            InitializeComponent();
            this.customerId = customerId;
        }


        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            BookingReservation booking = getBookingReservation();
            if (booking == null) return;

            FuminiHotelManagementContext.INSTANCE.BookingReservations.Add(booking);
            FuminiHotelManagementContext.INSTANCE.SaveChanges();
            MessageBox.Show("Booking Succesfully!");

        }
        private BookingReservation getBookingReservation()
        {
            BookingReservation booking = new BookingReservation();
            try
            {
                booking.BookingDate = dpkStartDate.SelectedDate.HasValue ? DateOnly.FromDateTime(dpkStartDate.SelectedDate.Value) : (DateOnly?)null;             
                booking.TotalPrice = decimal.TryParse(txtTotalPrice.Text, out decimal price) ? price : (decimal?)null;
                booking.CustomerId = customerId;
                booking.BookingStatus = 1;
                return booking;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
               return null;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cbxRoomType.ItemsSource = FuminiHotelManagementContext.INSTANCE.RoomTypes.ToList();
        }

        private void cbxRoomType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RoomType roomType = (RoomType)cbxRoomType.SelectedItem;
            cbxRoomNumber.ItemsSource = FuminiHotelManagementContext.INSTANCE.RoomInformations.Where(r => r.RoomTypeId == roomType.RoomTypeId).ToList();
        }

        private void count()
        {
            try
            {
                if (cbxRoomNumber == null) throw new Exception();
                RoomInformation room = (RoomInformation)cbxRoomNumber.SelectedItem;
                DateTime StartDate = dpkStartDate.SelectedDate ?? DateTime.Today;
                DateTime EndDate = dpkEndDate.SelectedDate ?? DateTime.Today;

                DateTime startDate = dpkStartDate.SelectedDate ?? DateTime.Today;
                DateTime endDate = dpkEndDate.SelectedDate ?? DateTime.Today;

                // Calculate the difference
                TimeSpan difference = endDate - startDate;

                // Access different components of the TimeSpan if needed
                int differenceInDays = (int)difference.TotalDays + 1;
                if (differenceInDays < 0) { txtTotalPrice.Text = "Wrong EndDate"; return; }

                if (room == null) throw new Exception();
                decimal total = (decimal)(differenceInDays * room.RoomPricePerDay);


                txtTotalPrice.Text = total.ToString();
            }
            catch (Exception ex)
            {
                if (txtTotalPrice != null)
                    txtTotalPrice.Text = "Fill Input";
            }


        }

        private void cbxRoomNumber_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            count();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            CustomerInfo customerinfo = new CustomerInfo(customerId);
            customerinfo.Show();
            this.Close();
        }
    }
}
