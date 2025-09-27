import { onLCP, onINP, onCLS, onFCP, onTTFB, CLSMetric, INPMetric, LCPMetric, FCPMetric, TTFBMetric } from 'web-vitals';

function reportWebVitals(onPerfEntry: {
  (metric: LCPMetric): void;
  (metric: INPMetric): void;
  (metric: CLSMetric): void;
  (metric: FCPMetric): void;
  (metric: TTFBMetric): void;
}) {
  if (onPerfEntry && typeof onPerfEntry === 'function') {
    onCLS(onPerfEntry);
    onFCP(onPerfEntry);
    onINP(onPerfEntry);
    onLCP(onPerfEntry);
    onTTFB(onPerfEntry);
  }
}

export default reportWebVitals;
