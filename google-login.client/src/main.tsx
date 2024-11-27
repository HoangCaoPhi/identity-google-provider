import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import App from './App.tsx'
import { GoogleOAuthProvider } from '@react-oauth/google'

const clientId = import.meta.env.VITE_REACT_APP_GOOGLE_CLIENT_ID; 
 
createRoot(document.getElementById('root')!).render(
  <StrictMode>
        <GoogleOAuthProvider clientId="966019251517-53eh4hk0gicdp9qj7nnkd263uau5ce58.apps.googleusercontent.com">
      <App />
    </GoogleOAuthProvider>,
  </StrictMode>,
)
