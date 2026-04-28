import { Component, ElementRef, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

interface Message {
  text: string;
  sender: 'user' | 'bot';
}

@Component({
  selector: 'app-chatbot',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './chatbot.html',
  styleUrl: './chatbot.css'
})
export class ChatbotComponent {
  abierto = false;
  input = '';

  mensajes: Message[] = [
    {
      sender: 'bot',
      text: '👋 Hola, somos Mecatronic Motors.\nSoy tu asistente virtual.\n¿En qué puedo ayudarte hoy?'
    }
  ];

  @ViewChild('chatBody') chatBody!: ElementRef<HTMLDivElement>;

  toggle() {
    this.abierto = !this.abierto;
    setTimeout(() => this.scrollBottom(), 50);
  }

  enviar() {
    const texto = this.input.trim();
    if (!texto) return;

    this.mensajes.push({ sender: 'user', text: texto });
    this.input = '';

    setTimeout(() => this.scrollBottom(), 10);

    const reply = this.botReply(texto);

    if (reply) {
      this.mensajes.push({ sender: 'bot', text: reply });
      setTimeout(() => this.scrollBottom(), 10);
    }
  }

  private scrollBottom() {
    if (!this.chatBody) return;
    const el = this.chatBody.nativeElement;
    el.scrollTop = el.scrollHeight;
  }

  abrirMapa() {
    const query =
      'Av. Angélica Gamarra Mz. I Lote 11B APV Los Libertadores, San Martín de Porres, Lima, Perú';
    window.open(
      `https://www.google.com/maps/search/?api=1&query=${encodeURIComponent(query)}`,
      '_blank'
    );
  }

  abrirWhatsAppReserva() {
    const phone = '51987654321';
    const msg =
      'Hola, quiero reservar una cita.\n\nNombre:\nPlaca:\nServicio:\nFecha/Hora:';
    window.open(`https://wa.me/${phone}?text=${encodeURIComponent(msg)}`, '_blank');
  }

  botReply(text: string): string | null {
    const t = text.toLowerCase().trim();

    if (t.includes('mapa')) {
      this.abrirMapa();
      return '🗺️ Abriendo mapa...';
    }

    if (t.includes('hola') || t.includes('buenas')) {
      return 'Puedo ayudarte con:\n• Horario\n• Ubicación\n• Reservas\n• Estado del vehículo\n\n¿Qué deseas consultar?';
    }

    if (t.includes('horario') || t.includes('atienden') || t.includes('atencion') || t.includes('atención')) {
      return '🕒 Atendemos de Lunes a Sábado, 9:00 a 18:00.';
    }

    if (t.includes('ubicacion') || t.includes('ubicación') || t.includes('direccion') || t.includes('dirección')) {
      return '📍 Estamos en:\nAv. Angélica Gamarra Mz. I – Lote 11B,\nAPV Los Libertadores, SMP - Lima.\n\nEscribe "mapa" para abrir la ubicación.';
    }

    if (t.includes('whatsapp reserva')) {
      this.abrirWhatsAppReserva();
      return '📲 Abriendo WhatsApp para reservar...';
    }

    if (t.includes('reserva') || t.includes('cita') || t.includes('agendar')) {
      return '✅ Para reservar te puedo abrir WhatsApp con el mensaje listo.\nEscribe: "whatsapp reserva"';
    }

    if (t.includes('estado') || t.includes('vehiculo') || t.includes('vehículo') || t.includes('auto') || t.includes('carro')) {
      return '🚗 Para consultar el estado necesito tu placa.\nEjemplo: ABC-123';
    }

    return 'Puedo ayudarte con: horario, ubicación, reservas y estado.\nPrueba: "horario", "ubicación", "reserva" o "estado".';
  }
}
