# Changelog

## 2.0.0

The version has been bumped to 2.0.0 as this release breaks build when upgrading from 1.4.0.

This version removes the Debug operator, which didn't work anyway.

ToColdObservable has been removed as it doesn't work as intended.

SelectSwitch has been renamed to SelectLatest, which is more akin to what the operator is called in other versions of Rx.

The non-generic version of GetEvents has been renamed to GetEventSignal and now returns an IObservable&lt;Unit&gt;

## 1.0.0

Initial release