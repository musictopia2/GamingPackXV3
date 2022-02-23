﻿namespace RiskBlazor;
public static class GraphicExtensions
{
    public static void DrawArmyPiece(this ISvg svg, string customColor)
    {
        svg.ViewBox = "0 0 2000 2000";
        G group = new();
        svg.Children.Add(group);
        Path path = new();
        path.Fill = customColor.ToWebColor(); //i think.
        path.D = "M1393.4,1896.5c14-8.8,19.3-21,15.8-36.8c-3.5-15.8-14-24.5-29.8-26.3c-12.3-1.8-22.8-3.5-35.1-7c-8.8-3.5-15.8-10.5-24.5-15.8c-5.3-3.5-10.5-7-15.8-10.5c-17.5-8.8-36.8-12.3-49.1-24.5c-14-12.3-19.3-31.6-29.8-47.3c-5.3-10.5-12.3-21-19.3-35.1c7-7,15.8-15.8,28-26.3c-3.5-10.5-8.8-24.5-14-36.8c-5.3-17.5-14-33.3-15.8-50.8c-3.5-40.3-1.8-78.9-1.8-119.2c0-19.3-1.8-40.3-3.5-59.6c-5.3-35.1-7-71.9-15.8-105.2c-7-28-21-52.6-29.8-78.9c-7-17.5-12.3-35.1-15.8-52.6c-3.5-17.5-8.8-28-22.8-38.6c-21-15.8-47.3-26.3-59.6-54.3c-17.5-38.6-40.3-73.6-61.3-110.4c-1.8-3.5,0-12.3,1.8-14c19.3-12.3,21-29.8,19.3-50.8c0-5.3,1.8-10.5,3.5-15.8c3.5-10.5,8.8-19.3,8.8-29.8c1.8-12.3-3.5-26.3,17.5-28c3.5,0,7-12.3,12.3-21c19.3,7,38.6,5.3,52.6-12.3c3.5-3.5,10.5-1.8,17.5-1.8c61.3,1.8,112.2-21,152.5-68.4c7-8.8,17.5-15.8,26.3-22.8c1.8-1.8,5.3-5.3,5.3-7c3.5-14,5.3-28,8.8-42.1c7-28,12.3-57.8,19.3-85.9c1.8-8.8,12.3-14,19.3-21c3.5-3.5,10.5-7,10.5-10.5c0-5.3-5.3-12.3-8.8-14c-7-5.3-15.8-7-24.5-10.5c0-1.8,0-3.5,0-7c15.8-5.3,29.8-10.5,45.6-17.5c3.5-1.8,7-8.8,10.5-12.3c-5.3-1.8-10.5-5.3-15.8-7c-1.8,0-5.3,1.8-7,1.8c-1.8,0-5.3,0-8.8-3.5c36.8-10.5,64.9-40.3,110.4-40.3c-12.3,35.1,17.5,35.1,35.1,43.8c7,3.5,19.3-3.5,29.8-7c10.5-1.8,19.3-7,29.8-7c59.6-1.8,119.2-1.8,177-3.5c8.8,0,21,5.3,21-14c-15.8,0-31.5,0-45.6,0c-21,0-43.8,1.8-64.9,0c-29.8-3.5-59.6-7-89.4-14c-3.5,0-7-12.3-8.8-17.5c17.5-3.5,29.8-8.8,42.1-10.5c28-1.8,54.3,0,82.4,0c8.8,0,19.3-1.8,28-3.5c0-1.8,1.8-5.3,1.8-7c-7-1.8-14-7-21-7c-33.3,0-64.9,1.8-98.2,3.5c-15.8,0-29.8,3.5-36.8-19.3c-3.5-14-29.8-17.5-50.8-15.8c-29.8,3.5-57.8,5.3-87.6,7c-3.5,0-7,1.8-12.3,1.8c-29.8,0-59.6,1.8-89.4,1.8c-5.3,0-8.8-7-14-8.8c0-1.8,0-5.3,1.8-7c14-3.5,28-5.3,43.8-8.8c8.8-31.6,0-42.1-33.3-38.6c-26.3,1.8-50.8,1.8-77.1,8.8c-8.8,1.8-24.5-10.5-33.3-19.3c-10.5-12.3-14-14-24.5-1.8c-1.8,1.8-3.5,7-5.3,7c-19.3,0-15.8,21-28,28c-8.8,5.3,0,17.5,14,19.3c17.5,1.8,35.1,3.5,52.6,8.8c-22.8,19.3-52.6,3.5-80.6,15.8c10.5,1.8,17.5,3.5,24.5,7c-45.6,31.6-96.4,22.8-147.2,21c10.5-19.3,19.3-35.1,29.8-52.6l1.8-3.5c36.8-10.5,15.8-31.6,7-49.1c0-1.8,0-1.8-1.8-7c7-1.8,14-5.3,22.8-7c12.3-1.8,14-8.8,12.3-19.3c-5.3-19.3-7-40.3-12.3-59.6c-1.8-8.8-7-17.5-10.5-24.5c-43.8-63.1-115.7-68.4-178.8-50.8c-19.3,5.3-33.3,15.8-42.1,35.1c-3.5,7-8.8,12.3-12.3,17.5c-5.3,10.5-10.5,22.8-14,35.1c-1.8,3.5,0,10.5-3.5,14c-15.8,24.5-3.5,40.3,14,54.3c17.5,15.8,28,31.6,22.8,56.1c-3.5,17.5-10.5,24.5-29.8,21c-5.3-1.8-10.5,0-17.5,0c0,12.3,3.5,22.8,0,31.6c-5.3,12.3-29.8,15.8-47.3,7c-21-12.3-29.8-12.3-40.3,10.5c-19.3,38.6-47.3,66.6-78.9,94.6c-12.3,10.5-19.3,24.5-14,43.8c1.8,3.5-3.5,10.5-7,15.8c-7,7-17.5,10.5-22.8,17.5c-5.3,8.8-7,22.8-8.8,33.3s1.8,21,0,33.3c7,14,5.3,38.6-7,52.6c-1.8,1.8-1.8,7-1.8,8.8c3.5,26.3,7,52.6,10.5,78.9c1.8,17.5,5.3,33.3,26.3,42.1c5.3,1.8,8.8,15.8,8.8,22.8c0,15.8,7,21,21,21c17.5,0,36.8,1.8,54.3,1.8c12.3,1.8,15.8,7,14,19.3c-1.8,21-1.8,43.8-3.5,66.6c-1.8,14-3.5,26.3-5.3,40.3c0,0,3.5,0,8.8-1.8c0,28,0,54.3-22.8,77.1c-24.5,22.8-33.3,54.3-35.1,87.6c-1.8,38.6-3.5,77.1-5.3,115.7c0,7,0,14,3.5,21c7,17.5,17.5,33.3,26.3,50.8c1.8,3.5,0,8.8,0,12.3c0,1.8-7,0-10.5,0c-5.3-1.8-10.5-7-15.8-7c-8.8,0-19.3-1.8-26.3,1.8c-14,7-26.3,17.5-36.8,28c-15.8,15.8-31.5,31.6-56.1,35.1c-7,1.8-12.3,10.5-21,14c-14,5.3-26.3,15.8-45.6,8.8c-8.8-3.5-22.8,1.8-33.3,5.3c-8.8,1.8-17.5,3.5-26.3,7c-7,1.8-12.3,7-17.5,10.5c-5.3,1.8-10.5,5.3-15.8,5.3c-26.3-5.3-54.3-10.5-80.6-17.5c-21-5.3-31.5-3.5-31.5,19.3c-1.8,29.8,1.8,59.6,1.8,91.1c0,15.8-1.8,33.3-7,47.3c-15.8,45.6-7,91.1,5.3,136.7c3.5,14,12.3,15.8,24.5,12.3c17.5-5.3,26.3-19.3,26.3-36.8c0-36.8,19.3-61.3,42.1-84.1c3.5-3.5,8.8-5.3,12.3-5.3c12.3,0,26.3,1.8,38.6,3.5c1.8,0,7,0,7,0c22.8,22.8,45.6,1.8,66.6-3.5c22.8-7,43.8-15.8,52.6-42.1c5.3-14,14-22.8,33.3-22.8c10.5,0,26.3-12.3,29.8-21c5.3-19.3,17.5-14,29.8-15.8c33.3-3.5,68.4-5.3,101.7-10.5c49.1-7,92.9-28,131.5-61.3c10.5-8.8,15.8-26.3,21-40.3c17.5-49.1,35.1-98.2,52.6-152.5c7,7,10.5,10.5,14,14c12.3,12.3,22.8,26.3,36.8,36.8c19.3,17.5,31.5,38.6,31.5,66.6c0,8.8,3.5,19.3,8.8,26.3c10.5,14,12.3,29.8,14,47.3c3.5,28,12.3,56.1,19.3,82.4c7,24.5,14,47.3,21,70.1c3.5,12.3,7,24.5,14,33.3c10.5,14,17.5,28,10.5,45.6c-1.8,3.5,0,10.5,3.5,14c10.5,14,26.3,26.3,17.5,47.3c22.8,19.3,7,42.1,5.3,61.3c-1.8,22.8-8.8,43.8-14,66.6c-5.3,21-3.5,26.3,19.3,28c15.8,1.8,33.3,3.5,49.1,3.5c19.3-1.8,42.1-12.3,59.6-7c21,7,40.3,8.8,59.6,7c17.5,0,35.1,3.5,52.6,5.3C1356.6,1896.5,1375.8,1896.5,1393.4,1896.5z M1297,515.3c1.8,0,3.5,0,5.3-1.8c8.8,12.3,19.3,22.8,14,42.1c-8.8,29.8-14,61.3-17.5,92.9c-3.5,21-7,36.8-26.3,49.1c-12.3,8.8-22.8,19.3-33.3,29.8c-31.5,26.3-68.4,42.1-106.9,52.6c-15.8,3.5-24.5-5.3-22.8-22.8c3.5-29.8,8.8-36.8,36.8-42.1c43.8-7,77.1-33.3,113.9-56.1c3.5-1.8,8.8-10.5,7-15.8c-3.5-19.3-7-38.6,5.3-57.8v-1.8c0-10.5,3.5-17.5,15.8-21c14-3.5,15.8-12.3,12.3-24.5C1297,531.1,1297,522.3,1297,515.3z M1090.1,468c-10.5-5.3-22.8-12.3-36.8-19.3c14-19.3,12.3-45.6,42.1-45.6c22.8,0,21,17.5,28,24.5C1112.9,440,1102.4,452.2,1090.1,468z M1225.1,452.2c0,15.8,3.5,33.3-1.8,47.3c-7,15.8-19.3,29.8-33.3,42.1c-10.5,8.8-24.5,15.8-42.1,7c15.8-10.5,33.3-15.8,40.3-28c8.8-14,7-33.3,7-50.8C1198.8,454,1205.8,450.5,1225.1,452.2z M1424.9,385.6c12.3,5.3,22.8,8.8,33.3,12.3c0,1.8-1.8,3.5-1.8,5.3C1433.7,408.4,1426.7,406.7,1424.9,385.6z";
        group.Children.Add(path);
    }
}