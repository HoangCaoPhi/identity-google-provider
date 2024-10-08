import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Button, CircularProgress, Card, CardContent, Typography, Box, Container } from '@mui/material';

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

const Home: React.FC = () => {
  const [weatherData, setWeatherData] = useState<WeatherForecast[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const navigate = useNavigate();

  const fetchWeatherData = async () => {
    try {
      const token = localStorage.getItem('jwt');

      const response = await fetch('api/weatherforecast', {
        method: 'GET',
        headers: {
          Authorization: `Bearer ${token}`,
          'Content-Type': 'application/json',
        },
      });

      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }

      const data: WeatherForecast[] = await response.json();
      setWeatherData(data);
      setLoading(false);
    } catch (error) {
      console.error('Error fetching weather data:', error);
      setLoading(false);
    }
  };

  const handleLogout = () => {
    localStorage.removeItem('jwt');
    navigate('/login');
  };

  useEffect(() => {
    fetchWeatherData();
  }, []);

  return (
    <Container sx={{ marginTop: 4 }}>
      <Box display="flex" justifyContent="space-between" alignItems="center" mb={4}>
        <Typography variant="h4" component="h1">
          Weather Forecast
        </Typography>
        <Button variant="contained" color="secondary" onClick={handleLogout}>
          Logout
        </Button>
      </Box>

      {loading ? (
        <Box display="flex" justifyContent="center" mt={4}>
          <CircularProgress />
        </Box>
      ) : (
        <Box display="grid" gridTemplateColumns="repeat(auto-fill, minmax(250px, 1fr))" gap={2}>
          {weatherData.map((forecast, index) => (
            <Card key={index} sx={{ maxWidth: 300, borderRadius: 2, boxShadow: 3 }}>
              <CardContent>
                <Typography variant="h6" gutterBottom>
                  {new Date(forecast.date).toLocaleDateString()}
                </Typography>
                <Typography variant="body1">
                  <strong>Temperature:</strong> {forecast.temperatureC}°C ({forecast.temperatureF}°F)
                </Typography>
                <Typography variant="body1">
                  <strong>Summary:</strong> {forecast.summary}
                </Typography>
              </CardContent>
            </Card>
          ))}
        </Box>
      )}
    </Container>
  );
};

export default Home;
