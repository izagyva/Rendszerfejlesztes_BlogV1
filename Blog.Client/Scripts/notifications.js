import { showNotification } from './notifications.js';

// Show a success notification
showNotification('Comment added successfully', 'success');

// Show an error notification
showNotification('Error adding comment', 'error');

import { getData } from './api.js';

async function onLoadNotifications() {
    const notifications = await getNotifications();
    const notificationListElement = document.getElementById('notification-list');
    notifications.forEach(notification => {
        const notificationElement = document.createElement('div');
        notificationElement.textContent = notification.text; // replace with your actual notification text property
        notificationListElement.appendChild(notificationElement);
    });
}

async function getNotifications() {
    const response = await getData('notifications');
    if (response.status === 200) {
        return response.json();
    } else {
        console.error('Error getting notifications:', response);
    }
}

export { getNotifications };
