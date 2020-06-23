using AppTrips.Models;
using AppTrips.Renders;
using AppTrips.UWP.Renders;
using Proyect_U.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls.Maps;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.UWP;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace AppTrips.UWP.Renders
{
    public class CustomMapRenderer : MapRenderer
    {
        MapControl nativeMap;
        MarkerWindow markerWindowStart;
        MarkerWindow markerWindowEnd;
        MarkerWindow markerWindow;
        bool markerWindowShown = false;
        UserModel User;
        TripModel Trip;

        protected override void OnElementChanged(ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                nativeMap.MapElementClick -= OnMapElementClick; 
                nativeMap.Children.Clear();
                markerWindow = null;
                nativeMap = null;
            }

            if (e.NewElement != null)
            {
                this.User = (e.NewElement as CustomMap).User;
                this.Trip = (e.NewElement as CustomMap).Trip;

                var formsMap = (CustomMap)e.NewElement;
                nativeMap = Control as MapControl;
                nativeMap.Children.Clear();
                nativeMap.MapElementClick += OnMapElementClick;
                renderMark(Trip.OriginCoordinates, "marker_opt.png");
                renderMark(User.CurrentLocation.Latitude + " " + User.CurrentLocation.Longitude, "marker_opt.png");
                renderMark(Trip.DestinationCoordinates, "marker_opt.png");
            }
        }

        private void renderMark(string location, string img)
        {
            string lat = location.Split(' ')[0];
            string lon = location.Split(' ')[1];

            var snPosition = new BasicGeoposition
            {
                Latitude = Double.Parse(lat),
                Longitude = Double.Parse(lon)
            };
            var snPoint = new Geopoint(snPosition);

            var mapIcon = new MapIcon();
            mapIcon.Image = RandomAccessStreamReference.CreateFromUri(new Uri($"ms-appx:///{img}"));
            mapIcon.CollisionBehaviorDesired = MapElementCollisionBehavior.RemainVisible;
            mapIcon.Location = snPoint;
            mapIcon.NormalizedAnchorPoint = new Windows.Foundation.Point(0.5, 1.0);

            nativeMap.MapElements.Add(mapIcon);

        }

        private void OnMapElementClick(MapControl sender, MapElementClickEventArgs args)
        {
            var mapIcon = args.MapElements.FirstOrDefault(x => x is MapIcon) as MapIcon;

            if(mapIcon != null)
            {
                if (!markerWindowShown)
                {
                    if (markerWindow == null) markerWindow = new MarkerWindow(Trip);

                    var snPosition = new BasicGeoposition
                    {
                        Latitude = Trip.Latitude,
                        Longitude = Trip.Longitude
                    };
                    var snPoint = new Geopoint(snPosition);

                    nativeMap.Children.Add(markerWindow);
                    MapControl.SetLocation(markerWindow, snPoint);
                    MapControl.SetNormalizedAnchorPoint(markerWindow, new Windows.Foundation.Point(0.5, 1.0));

                    markerWindowShown = true;
                }
                else
                {
                    nativeMap.Children.Remove(markerWindow);
                    markerWindowShown = false;
                }
            }
        }
    }
}
